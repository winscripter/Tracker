using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tracker.Tracking;
using Tracker.Tracking.Format;
using Tracker.ViewModels;

namespace Tracker.Explorer
{
    public partial class TrackingTargetPreview : UserControl
    {
        private const int NumberOfHexCharacters = 6;
        private const int DefaultPercentage = 0;

        private static readonly string s_hexCharacters = "1234567890ABCDEF";

        public ICommand AddTableCommand { get; set; }
        public ICommand RemoveTableCommand { get; set; }
        public ICommand AddMemberCommand { get; set; }
        public ICommand RemoveMemberCommand { get; set; }
        public ICommand CustomizeMemberCommand { get; set; }
        public TrackingTargetFile File { get; set; }

        public string? CurrentTable { get; set; } = null;

        public TrackingTargetPreview()
        {
            File = TrackingTargetFile.Empty;
            AddTableCommand = new RelayCommand(
                e => AddTableCore(),
                e => true
            );
            RemoveTableCommand = new RelayCommand(
                e => RemoveTableCore(),
                e => true
            );
            AddMemberCommand = new RelayCommand(
                e => AddMemberCore(),
                e => true
            );
            RemoveMemberCommand = new RelayCommand(
                e => RemoveMemberCore(),
                e => true
            );
            CustomizeMemberCommand = new RelayCommand(
                e => CustomizeMemberCore(),
                e => true
            );
            InitializeComponent();
            DataContext = this;
        }

        public static TrackingTargetPreview CreateFromFile(TrackingTargetFile file)
        {
            var ttp = new TrackingTargetPreview
            {
                File = file
            };
            ttp.UpdateTables();
            return ttp;
        }

        private static TextBlock CreateTableCueContext(string name)
        {
            var tb = new TextBlock
            {
                Foreground = Brushes.DodgerBlue,
                FontSize = 18D,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(10D, 10D, 0D, 0D),
                Text = name
            };
            return tb;
        }

        internal void UpdateTables()
        {
            trackingTableList.trackingTargetList.Children.Clear();
            foreach (TrackingTableModel table in File.Model.Tables)
            {
                TrackingTargetTableControl tableControl = new()
                {
                    DisplayMemberName = table.Name ?? "???",
                };
                tableControl.MouseLeftButtonDown += (s, e) =>
                {
                    LMBDownOnTable(table.Name ?? "???");
                };
                trackingTableList.trackingTargetList.Children.Add(tableControl);
            }
        }

        private void LMBDownOnTable(string name)
        {
            TrackingTableModel ttm = File.Model.Tables.First(table => table.Name == name);
            SetContents(ttm);
        }

        private void SetContents(TrackingTableModel table)
        {
            memberList.Children.Clear();
            memberList.Children.Add(CreateTableCueContext(table.Name ?? "(Unknown)"));
            foreach (TrackingMemberModel member in table.Members.OrderBy(n => n.Name))
            {
                var memberCtl = new TrackingTargetMemberControl()
                {
                    EllipseFill = (SolidColorBrush?)Cache.BrushConverter.ConvertFrom(member.Value!.HexColor!) ?? Brushes.Transparent,
                    DisplayName = member.Name ?? "???",
                    Done = member.Value!.Percentage + "%"
                };
                if (member.Done)
                {
                    memberCtl.displayText.Foreground = Brushes.Green;
                }
                memberCtl.UpdateDone();
                memberList.Children.Add(memberCtl);
            }
            CurrentTable = table.Name ?? "???";
        }

        private static string GenerateRandomHex()
        {
            var sb = new StringBuilder();
            sb.Append('#');
            for (int i = 0; i < NumberOfHexCharacters; i++)
            {
                sb.Append(s_hexCharacters[Random.Shared.Next(1, s_hexCharacters.Length) - 1]);
            }
            return sb.ToString();
        }

        private static string? AskForName()
        {
            var dialog = new EnterNameDialog();
            dialog.ShowDialog();

            return dialog.NameResult;
        }

        private void AddTableCore()
        {
            string? result = AskForName();
            if (result is string actualResult)
            {
                if (File.Model.Tables.Any(table => table.Name == actualResult))
                {
                    MessageBox.Show(
                        $"Table named '{actualResult}' already exists",
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
                else
                {
                    File.Model.Tables.Add(new TrackingTableModel()
                    {
                        Members = [],
                        Name = actualResult,
                        PrimaryHexColor = GenerateRandomHex()
                    });
                    UpdateTables();
                }
            }
        }

        private void RemoveTableCore()
        {
            string? result = AskForName();
            if (result is string actualResult)
            {
                if (!File.Model.Tables.Any(table => table.Name == actualResult))
                {
                    MessageBox.Show(
                        $"Table named '{actualResult}' does not exist",
                        "Tracker",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
                else
                {
                    File.Model.Tables = (
                        from table in File.Model.Tables
                        where table.Name != actualResult
                        select table
                    ).ToList();
                    UpdateTables();
                }
            }
        }

        private void AddMemberCore()
        {
            if (CurrentTable is null)
            {
                MessageBox.Show(
                    "No table is selected right now.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            string? result = AskForName();
            if (result is string actualResult)
            {
                TrackingTableModel? table = File.Model.Tables.FirstOrDefault(table => table.Name == CurrentTable);
                bool succeeded = false;
                if (table is not null)
                {
                    TrackingMemberModel? member = table.Members.FirstOrDefault(member => member.Name == actualResult);
                    if (member is not null)
                    {
                        MessageBox.Show(
                            $"Member named '{actualResult}' already exists inside the table '{CurrentTable}'",
                            "Tracker",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }
                    else
                    {
                        var model = new TrackingMemberModel()
                        {
                            Name = actualResult,
                            Done = false,
                            Value = new TrackingValueModel()
                            {
                                HexColor = GenerateRandomHex(),
                                Percentage = DefaultPercentage
                            }
                        };
                        table.Members.Add(model);
                        succeeded = true;
                    }
                }

                if (succeeded)
                {
                    var newList = new List<TrackingTableModel>();
                    foreach (TrackingTableModel model in File.Model.Tables)
                    {
                        if (model.Name == table!.Name)
                        {
                            newList.Add(table);
                        }
                        else
                        {
                            newList.Add(model);
                        }
                    }
                    newList = [.. newList.OrderBy(model => model.Name)];
                    File.Model.Tables = newList;
                    SetContents(table!);
                }
            }
        }

        private void RemoveMemberCore()
        {
            if (CurrentTable is null)
            {
                MessageBox.Show(
                    "No table is selected right now.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            string? result = AskForName();
            if (result is string actualResult)
            {
                TrackingTableModel? table = File.Model.Tables.FirstOrDefault(table => table.Name == CurrentTable);
                if (table is not null)
                {
                    TrackingMemberModel? member = table.Members.FirstOrDefault(member => member.Name == actualResult);
                    if (member is null)
                    {
                        MessageBox.Show(
                            $"Member named '{actualResult}' does not exist inside the table '{CurrentTable}'",
                            "Tracker",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }

                    table.Members =
                    [
                        ..table.Members.Where(member => member.Name != actualResult)
                               .OrderBy(member => member.Name)
                    ];
                }

                var newList = new List<TrackingTableModel>();
                foreach (TrackingTableModel model in File.Model.Tables)
                {
                    if (model.Name == table!.Name)
                    {
                        newList.Add(table);
                    }
                    else
                    {
                        newList.Add(model);
                    }
                }
                newList = [.. newList.OrderBy(model => model.Name)];
                File.Model.Tables = newList;
                SetContents(table!);
            }
        }

        private void CustomizeMemberCore()
        {
            if (CurrentTable is null)
            {
                MessageBox.Show(
                    "No table is selected right now.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            string? result = AskForName();
            if (result is string actualResult)
            {
                TrackingTableModel? table = File.Model.Tables.FirstOrDefault(table => table.Name == CurrentTable);
                if (table is not null)
                {
                    TrackingMemberModel? member = table.Members.FirstOrDefault(member => member.Name == actualResult);
                    if (member is null)
                    {
                        MessageBox.Show(
                            $"Member named '{actualResult}' does not exist inside the table '{CurrentTable}'",
                            "Tracker",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }

                    try
                    {
                        var window = new CustomizeMemberWindow();

                        window.ValueCustomizationView.name.Text = member?.Name ?? "???";
                        window.ValueCustomizationView.completedBox.Text = (member?.Value?.Percentage ?? 0).ToString();

                        window.ColorCustomizationView.hexValue.Text = member?.Value?.HexColor ?? "#FFFFFF";

                        window.contentPresenter.Content = window.ColorCustomizationView;
                        window.ShowDialog();

                        if (window.Returned)
                        {
                            member!.Value!.HexColor = window.ColorCustomizationView.hexValue.Text;
                            member!.Value!.Percentage = int.Parse(window.ValueCustomizationView.completedBox.Text);
                            member!.Name = window.ValueCustomizationView.name.Text;

                            table.Members =
                            [
                                ..table.Members.Where(m => m.Name != member?.Name)
                                  .Append(member)
                                  .OrderBy(member => member?.Name)
                            ];

                            var newList = new List<TrackingTableModel>();
                            foreach (TrackingTableModel model in File.Model.Tables)
                            {
                                if (model.Name == table!.Name)
                                {
                                    newList.Add(table);
                                }
                                else
                                {
                                    newList.Add(model);
                                }
                            }
                            newList = [.. newList.OrderBy(model => model.Name)];
                            File.Model.Tables = newList;
                            SetContents(table!);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.ToString(),
                            "Tracker",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }
                }
            }
        }
    }
}
