using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RoyalFamilyTree
{
    public partial class MainWindow : Window
    {
        private RoyalFamilyTree familyTree;
        private const double NODE_WIDTH = 150;
        private const double NODE_HEIGHT = 80;
        private const double HORIZONTAL_SPACING = 30;
        private const double VERTICAL_SPACING = 100;

        public MainWindow()
        {
            InitializeComponent();
            InitializeRoyalFamily();
            DrawTree();
        }

        /// <summary>
        /// Initialize the Royal Family tree with the House of Windsor members
        /// </summary>
        private void InitializeRoyalFamily()
        {
            // Create the monarch (root)
            var charlesIII = new RoyalFamilyMember("King Charles III", new DateTime(1948, 11, 14), true, "King");
            familyTree = new RoyalFamilyTree(charlesIII);

            // Add William, Prince of Wales
            var william = new FamilyTreeNode(new RoyalFamilyMember("William", new DateTime(1982, 6, 21), true, "Prince of Wales"));
            familyTree.Root.AddChild(william);

            // William's children
            var george = new FamilyTreeNode(new RoyalFamilyMember("Prince George", new DateTime(2013, 7, 22), true));
            var charlotte = new FamilyTreeNode(new RoyalFamilyMember("Princess Charlotte", new DateTime(2015, 5, 2), true));
            var louis = new FamilyTreeNode(new RoyalFamilyMember("Prince Louis", new DateTime(2018, 4, 23), true));
            william.AddChild(george);
            william.AddChild(charlotte);
            william.AddChild(louis);

            // Add Harry, Duke of Sussex
            var harry = new FamilyTreeNode(new RoyalFamilyMember("Harry", new DateTime(1984, 9, 15), true, "Duke of Sussex"));
            familyTree.Root.AddChild(harry);

            // Harry's children
            var archie = new FamilyTreeNode(new RoyalFamilyMember("Prince Archie", new DateTime(2019, 5, 6), true));
            var lilibet = new FamilyTreeNode(new RoyalFamilyMember("Princess Lilibet", new DateTime(2021, 6, 4), true));
            harry.AddChild(archie);
            harry.AddChild(lilibet);

            // Add Andrew, Duke of York
            var andrew = new FamilyTreeNode(new RoyalFamilyMember("Andrew", new DateTime(1960, 2, 19), true, "Duke of York"));
            familyTree.Root.AddChild(andrew);

            // Andrew's children
            var beatrice = new FamilyTreeNode(new RoyalFamilyMember("Princess Beatrice", new DateTime(1988, 8, 8), true));
            var eugenie = new FamilyTreeNode(new RoyalFamilyMember("Princess Eugenie", new DateTime(1990, 3, 23), true));
            andrew.AddChild(beatrice);
            andrew.AddChild(eugenie);

            // Beatrice's children
            var sienna = new FamilyTreeNode(new RoyalFamilyMember("Sienna", new DateTime(2021, 9, 18), true));
            beatrice.AddChild(sienna);

            // Eugenie's children
            var august = new FamilyTreeNode(new RoyalFamilyMember("August", new DateTime(2021, 2, 9), true));
            var ernest = new FamilyTreeNode(new RoyalFamilyMember("Ernest", new DateTime(2023, 5, 30), true));
            eugenie.AddChild(august);
            eugenie.AddChild(ernest);

            // Add Edward, Duke of Edinburgh
            var edward = new FamilyTreeNode(new RoyalFamilyMember("Edward", new DateTime(1964, 3, 10), true, "Duke of Edinburgh"));
            familyTree.Root.AddChild(edward);

            // Edward's children
            var james = new FamilyTreeNode(new RoyalFamilyMember("James", new DateTime(2007, 12, 17), true, "Earl of Wessex"));
            var louise = new FamilyTreeNode(new RoyalFamilyMember("Lady Louise", new DateTime(2003, 11, 8), true));
            edward.AddChild(louise);
            edward.AddChild(james);
        }

        /// <summary>
        /// Draws the family tree on the canvas
        /// </summary>
        private void DrawTree()
        {
            treeCanvas.Children.Clear();

            if (familyTree.Root == null) return;

            // Calculate positions for all nodes
            Dictionary<FamilyTreeNode, Point> positions = CalculateNodePositions();

            // Draw connections first (so they appear behind nodes)
            foreach (var kvp in positions)
            {
                var node = kvp.Key;
                var position = kvp.Value;

                foreach (var child in node.Children)
                {
                    if (positions.ContainsKey(child))
                    {
                        DrawConnection(position, positions[child]);
                    }
                }
            }

            // Draw nodes
            foreach (var kvp in positions)
            {
                DrawNode(kvp.Key, kvp.Value);
            }

            // Adjust canvas size
            double maxX = 0, maxY = 0;
            foreach (var pos in positions.Values)
            {
                maxX = Math.Max(maxX, pos.X + NODE_WIDTH);
                maxY = Math.Max(maxY, pos.Y + NODE_HEIGHT);
            }
            treeCanvas.Width = Math.Max(maxX + 50, treeCanvas.ActualWidth);
            treeCanvas.Height = Math.Max(maxY + 50, treeCanvas.ActualHeight);
        }

        /// <summary>
        /// Calculates positions for all nodes in the tree
        /// </summary>
        private Dictionary<FamilyTreeNode, Point> CalculateNodePositions()
        {
            var positions = new Dictionary<FamilyTreeNode, Point>();
            var levelNodes = new Dictionary<int, List<FamilyTreeNode>>();

            // Group nodes by level
            GroupNodesByLevel(familyTree.Root, 0, levelNodes);

            // Calculate positions level by level
            double currentY = 50;
            foreach (var level in levelNodes.Keys)
            {
                var nodes = levelNodes[level];
                double totalWidth = nodes.Count * NODE_WIDTH + (nodes.Count - 1) * HORIZONTAL_SPACING;
                double currentX = Math.Max(50, (treeCanvas.ActualWidth - totalWidth) / 2);

                foreach (var node in nodes)
                {
                    positions[node] = new Point(currentX, currentY);
                    currentX += NODE_WIDTH + HORIZONTAL_SPACING;
                }

                currentY += NODE_HEIGHT + VERTICAL_SPACING;
            }

            return positions;
        }

        /// <summary>
        /// Groups nodes by their depth level
        /// </summary>
        private void GroupNodesByLevel(FamilyTreeNode node, int level, Dictionary<int, List<FamilyTreeNode>> levelNodes)
        {
            if (node == null) return;

            if (!levelNodes.ContainsKey(level))
            {
                levelNodes[level] = new List<FamilyTreeNode>();
            }
            levelNodes[level].Add(node);

            foreach (var child in node.Children)
            {
                GroupNodesByLevel(child, level + 1, levelNodes);
            }
        }

        /// <summary>
        /// Draws a connection line between parent and child nodes
        /// </summary>
        private void DrawConnection(Point parentPos, Point childPos)
        {
            Line line = new Line
            {
                X1 = parentPos.X + NODE_WIDTH / 2,
                Y1 = parentPos.Y + NODE_HEIGHT,
                X2 = childPos.X + NODE_WIDTH / 2,
                Y2 = childPos.Y,
                Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
                StrokeThickness = 2
            };
            treeCanvas.Children.Add(line);
        }

        /// <summary>
        /// Draws a node representing a family member
        /// </summary>
        private void DrawNode(FamilyTreeNode node, Point position)
        {
            // Create border
            Border border = new Border
            {
                Width = NODE_WIDTH,
                Height = NODE_HEIGHT,
                Background = node.Member.IsAlive ?
                    new SolidColorBrush(Color.FromRgb(200, 230, 201)) :
                    new SolidColorBrush(Color.FromRgb(220, 220, 220)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(66, 66, 66)),
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(5)
            };

            // Create text content
            StackPanel stackPanel = new StackPanel
            {
                Margin = new Thickness(5)
            };

            TextBlock nameText = new TextBlock
            {
                Text = node.Member.Name,
                FontWeight = FontWeights.Bold,
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center
            };

            TextBlock titleText = new TextBlock
            {
                Text = node.Member.Title,
                FontSize = 10,
                FontStyle = FontStyles.Italic,
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100))
            };

            TextBlock dobText = new TextBlock
            {
                Text = $"Born: {node.Member.DateOfBirth:yyyy}",
                FontSize = 9,
                TextAlignment = TextAlignment.Center
            };

            TextBlock statusText = new TextBlock
            {
                Text = node.Member.IsAlive ? "✓ Living" : "✗ Deceased",
                FontSize = 9,
                TextAlignment = TextAlignment.Center,
                Foreground = node.Member.IsAlive ?
                    new SolidColorBrush(Color.FromRgb(56, 142, 60)) :
                    new SolidColorBrush(Color.FromRgb(183, 28, 28))
            };

            stackPanel.Children.Add(nameText);
            if (!string.IsNullOrEmpty(node.Member.Title))
            {
                stackPanel.Children.Add(titleText);
            }
            stackPanel.Children.Add(dobText);
            stackPanel.Children.Add(statusText);

            border.Child = stackPanel;

            Canvas.SetLeft(border, position.X);
            Canvas.SetTop(border, position.Y);

            treeCanvas.Children.Add(border);
        }

        /// <summary>
        /// Search using Breadth-First Search
        /// </summary>
        private void BtnSearchBFS_Click(object sender, RoutedEventArgs e)
        {
            string searchName = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchName))
            {
                MessageBox.Show("Please enter a name to search.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = familyTree.SearchBFS(searchName);
            if (result != null)
            {
                int position = familyTree.GetSuccessionPosition(searchName);
                string positionText = position > 0 ? $"\nPosition in line to throne: {position}" : "\nNot in line of succession";

                txtSearchResult.Text = $"Found (BFS):\n{result.Member}\n{positionText}";
                txtSearchResult.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txtSearchResult.Text = $"Member '{searchName}' not found.";
                txtSearchResult.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        /// <summary>
        /// Search using Depth-First Search
        /// </summary>
        private void BtnSearchDFS_Click(object sender, RoutedEventArgs e)
        {
            string searchName = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchName))
            {
                MessageBox.Show("Please enter a name to search.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = familyTree.SearchDFS(searchName);
            if (result != null)
            {
                int position = familyTree.GetSuccessionPosition(searchName);
                string positionText = position > 0 ? $"\nPosition in line to throne: {position}" : "\nNot in line of succession";

                txtSearchResult.Text = $"Found (DFS):\n{result.Member}\n{positionText}";
                txtSearchResult.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txtSearchResult.Text = $"Member '{searchName}' not found.";
                txtSearchResult.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        /// <summary>
        /// Add a new family member
        /// </summary>
        private void BtnAddMember_Click(object sender, RoutedEventArgs e)
        {
            string parentName = txtParentName.Text.Trim();
            string childName = txtChildName.Text.Trim();

            if (string.IsNullOrEmpty(parentName) || string.IsNullOrEmpty(childName))
            {
                MessageBox.Show("Please enter both parent and child names.", "Add Member", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date of birth.", "Add Member", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Find parent node
            var parentNode = familyTree.SearchBFS(parentName);
            if (parentNode == null)
            {
                MessageBox.Show($"Parent '{parentName}' not found in the family tree.", "Add Member", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create new member
            var newMember = new RoyalFamilyMember(
                childName,
                dpDateOfBirth.SelectedDate.Value,
                chkIsAlive.IsChecked ?? true,
                txtTitle.Text.Trim()
            );

            var newNode = new FamilyTreeNode(newMember);
            parentNode.AddChild(newNode);

            MessageBox.Show($"Successfully added {childName} as a child of {parentName}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear form and refresh tree
            txtParentName.Clear();
            txtChildName.Clear();
            txtTitle.Clear();
            dpDateOfBirth.SelectedDate = null;
            chkIsAlive.IsChecked = true;

            DrawTree();
        }

        /// <summary>
        /// Show the line of succession
        /// </summary>
        private void BtnShowSuccession_Click(object sender, RoutedEventArgs e)
        {
            var succession = familyTree.GetLineOfSuccession();

            if (succession.Count == 0)
            {
                MessageBox.Show("No living heirs found in the line of succession.", "Line of Succession", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string message = "Line of Succession to the Throne:\n\n";
            for (int i = 0; i < succession.Count; i++)
            {
                message += $"{i + 1}. {succession[i].Member.Name}";
                if (!string.IsNullOrEmpty(succession[i].Member.Title))
                {
                    message += $" ({succession[i].Member.Title})";
                }
                message += $" - Born {succession[i].Member.DateOfBirth:yyyy}\n";
            }

            MessageBox.Show(message, "Line of Succession", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Refresh the tree view
        /// </summary>
        private void BtnRefreshTree_Click(object sender, RoutedEventArgs e)
        {
            DrawTree();
            MessageBox.Show("Tree view refreshed successfully!", "Refresh", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}