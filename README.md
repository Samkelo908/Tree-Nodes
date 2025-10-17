# Royal Family Tree Visualization Tool

A dynamic C# WPF application for visualizing and managing the British Royal Family tree (House of Windsor).

![.NET](https://img.shields.io/badge/.NET-6.0-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![License](https://img.shields.io/badge/License-Educational-green)

## 📋 Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [How to Use](#how-to-use)
- [Project Structure](#project-structure)
- [Technical Details](#technical-details)
- [Screenshots](#screenshots)
- [Assignment Requirements](#assignment-requirements)
- [Troubleshooting](#troubleshooting)

## 🎯 Overview

This application provides an interactive visualization of the Royal Family tree with features including:
- Visual hierarchical tree display
- Breadth-First Search (BFS) and Depth-First Search (DFS) algorithms
- Dynamic member addition through GUI
- Automatic line of succession calculation
- Real-time tree updates

## ✨ Features

### Core Functionality

#### 1. Visual Tree Display
- **Hierarchical Layout**: Multi-generational family tree structure
- **Color-Coded Nodes**: 
  - 🟢 Green = Living members
  - ⚪ Grey = Deceased members
- **Node Information**: Name, title, birth year, and living status
- **Connection Lines**: Visual parent-child relationships
- **Scrollable Canvas**: Handle large family trees

#### 2. Search Algorithms

**Breadth-First Search (BFS)**
- Searches level by level through the tree
- Efficient for finding close relatives
- Uses queue data structure
- Returns succession position

**Depth-First Search (DFS)**
- Searches depth-first through each branch
- Thorough branch exploration
- Uses recursive implementation
- Returns succession position

#### 3. Dynamic Member Addition
- Add new family members through GUI forms
- Input validation for all fields
- Automatic tree refresh after addition
- Parent lookup verification
- Optional title field support

#### 4. Line of Succession
- Calculates complete succession order
- Follows primogeniture rules
- Shows only living members
- Updates dynamically when members are added
- Displays positions for all eligible heirs

#### 5. User-Friendly Interface
- Clean, intuitive layout
- Color-coded feedback
- Real-time updates
- Error messages for invalid input
- Professional design

## 💻 Requirements

### Software Requirements
- **Operating System**: Windows 10 or later
- **Development Tool**: Visual Studio 2022 (Community Edition or higher)
- **.NET Framework**: .NET 6.0 SDK or later
- **Workload**: .NET Desktop Development

### Hardware Requirements
- **Processor**: 1.8 GHz or faster
- **RAM**: 4 GB minimum (8 GB recommended)
- **Disk Space**: 500 MB for application and dependencies
- **Display**: 1280x720 resolution or higher

## 🚀 Installation

### Method 1: Visual Studio (Recommended)

1. **Download/Clone the Project**
   ```bash
   # If using Git
   git clone <repository-url>
   cd RoyalFamilyTree
   ```

2. **Open in Visual Studio**
   - Launch Visual Studio 2022
   - Click **File → Open → Project/Solution**
   - Navigate to project folder
   - Select `RoyalFamilyTree.csproj`
   - Click **Open**

3. **Restore Dependencies**
   ```
   Visual Studio will automatically restore NuGet packages
   ```

4. **Build the Project**
   - Press `Ctrl+Shift+B` or
   - Click **Build → Build Solution**

5. **Run the Application**
   - Press `F5` or
   - Click the green **Start** button

### Method 2: Command Line

1. **Navigate to Project Folder**
   ```bash
   cd RoyalFamilyTree
   ```

2. **Restore, Build, and Run**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## 📖 How to Use

### Searching for Family Members

1. **Using BFS (Breadth-First Search)**
   - Enter member name in search box (e.g., "William")
   - Click **Search (BFS)** button
   - View results showing member details and succession position

2. **Using DFS (Depth-First Search)**
   - Enter member name in search box
   - Click **Search (DFS)** button
   - View results with succession information

**Search Tips:**
- Search is case-insensitive
- Use first names or full names
- Examples: "William", "King Charles III", "Prince George"

### Adding New Family Members

1. Fill in the form fields:
   - **Parent Name**: Name of existing family member (must be in tree)
   - **Child Name**: Name of new member to add
   - **Date of Birth**: Select from date picker
   - **Is Alive**: Check if living, uncheck if deceased
   - **Title** (optional): Royal title (e.g., "Prince", "Princess")

2. Click **Add Member** button

3. Tree will automatically refresh with new member

**Validation Rules:**
- Parent must exist in the tree
- All required fields must be filled
- Date of birth must be selected

### Viewing Line of Succession

1. Click **Show Line of Succession** button
2. Popup window displays complete ordered list
3. Shows only living members
4. Includes position numbers

### Refreshing the Tree

- Click **Refresh Tree View** to manually redraw the tree
- Useful after multiple additions or window resize

## 📁 Project Structure

```
RoyalFamilyTree/
│
├── RoyalFamilyMember.cs          # Family member data model
├── FamilyTreeNode.cs             # Tree node structure
├── RoyalFamilyTree.cs            # Tree management & algorithms
│
├── MainWindow.xaml               # GUI layout (WPF)
├── MainWindow.xaml.cs            # GUI logic and event handlers
│
├── App.xaml                      # Application entry point
├── App.xaml.cs                   # Application code-behind
│
├── RoyalFamilyTree.csproj        # Project configuration
│
└── README.md                     # This file
```

## 🔧 Technical Details

### Class Architecture

#### RoyalFamilyMember Class
```csharp
public class RoyalFamilyMember
{
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsAlive { get; set; }
    public string Title { get; set; }
    public int Age { get; }  // Calculated property
}
```

**Purpose**: Represents a single royal family member with their attributes.

#### FamilyTreeNode Class
```csharp
public class FamilyTreeNode
{
    public RoyalFamilyMember Member { get; set; }
    public List<FamilyTreeNode> Children { get; set; }
    public FamilyTreeNode Parent { get; set; }
    
    public void AddChild(FamilyTreeNode child)
    public bool RemoveChild(FamilyTreeNode child)
    public int GetDepth()
}
```

**Purpose**: Represents a node in the tree structure with parent-child relationships.

#### RoyalFamilyTreeManager Class
```csharp
public class RoyalFamilyTreeManager
{
    public FamilyTreeNode Root { get; private set; }
    
    // Search Methods
    public FamilyTreeNode SearchBFS(string name)
    public FamilyTreeNode SearchDFS(string name)
    
    // Succession Methods
    public List<FamilyTreeNode> GetLineOfSuccession()
    public int GetSuccessionPosition(string name)
    
    // Traversal Methods
    public List<FamilyTreeNode> GetAllNodesBFS()
    public List<FamilyTreeNode> GetAllNodesDFS()
}
```

**Purpose**: Manages the entire tree structure and implements search algorithms.

### Algorithm Implementations

#### Breadth-First Search (BFS)
```csharp
public FamilyTreeNode SearchBFS(string name)
{
    Queue<FamilyTreeNode> queue = new Queue<FamilyTreeNode>();
    queue.Enqueue(Root);
    
    while (queue.Count > 0)
    {
        FamilyTreeNode current = queue.Dequeue();
        if (current.Member.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            return current;
        
        foreach (var child in current.Children)
            queue.Enqueue(child);
    }
    return null;
}
```
**Time Complexity**: O(n) where n is the number of nodes  
**Space Complexity**: O(w) where w is the maximum width of the tree

#### Depth-First Search (DFS)
```csharp
public FamilyTreeNode SearchDFS(string name)
{
    return SearchDFSRecursive(Root, name);
}

private FamilyTreeNode SearchDFSRecursive(FamilyTreeNode node, string name)
{
    if (node == null) return null;
    if (node.Member.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        return node;
    
    foreach (var child in node.Children)
    {
        FamilyTreeNode result = SearchDFSRecursive(child, name);
        if (result != null) return result;
    }
    return null;
}
```
**Time Complexity**: O(n) where n is the number of nodes  
**Space Complexity**: O(h) where h is the height of the tree (recursion stack)

### Pre-loaded Family Data

The application initializes with **20 members** of the House of Windsor:

```
King Charles III (Monarch)
├── William, Prince of Wales
│   ├── Prince George (2013)
│   ├── Princess Charlotte (2015)
│   └── Prince Louis (2018)
├── Harry, Duke of Sussex
│   ├── Prince Archie (2019)
│   └── Princess Lilibet (2021)
├── Andrew, Duke of York
│   ├── Princess Beatrice (1988)
│   │   └── Sienna (2021)
│   └── Princess Eugenie (1990)
│       ├── August (2021)
│       └── Ernest (2023)
└── Edward, Duke of Edinburgh
    ├── Lady Louise Windsor (2003)
    └── James, Earl of Wessex (2007)
```

## 📸 Screenshots

### Main Application Window
```
┌─────────────────────────────────────────────────┐
│  Royal Family Tree Visualization               │
├──────────────┬──────────────────────────────────┤
│              │    House of Windsor              │
│  Search      │                                  │
│  Controls    │      King Charles III            │
│              │            │                     │
│  Add Member  │      ┌─────┼─────┬─────┐        │
│  Form        │   William Harry Andrew Edward   │
│              │      │                           │
│  Display     │   ┌──┼──┬──┐                    │
│  Options     │ George Charlotte Louis          │
└──────────────┴──────────────────────────────────┘
```

### Search Results
```
Search Result:
Found (BFS):
William (Prince of Wales), Born: 1982-06-21, Alive
Position in line to throne: 1
```

### Line of Succession
```
Line of Succession to the Throne:

1. William (Prince of Wales) - Born 1982
2. Prince George - Born 2013
3. Princess Charlotte - Born 2015
4. Prince Louis - Born 2018
5. Harry (Duke of Sussex) - Born 1984
6. Prince Archie - Born 2019
7. Princess Lilibet - Born 2021
...
```

## ✅ Assignment Requirements

| Requirement | Status | Details |
|------------|--------|---------|
| Define RoyalFamilyMember class | ✅ | With name, DOB, isAlive attributes |
| Define FamilyTreeNode class | ✅ | With member and children |
| Initialize with monarch | ✅ | King Charles III as root |
| **BONUS**: Dynamic addition via GUI | ✅ | Full form with validation |
| Visual tree display | ✅ | Canvas-based rendering |
| Search with throne position | ✅ | Both BFS and DFS |
| Easy-to-use GUI | ✅ | Professional WPF interface |
| Dynamic tree management | ✅ | Real-time updates |
| Include BFS | ✅ | Queue-based implementation |
| Include DFS | ✅ | Recursive implementation |

## 🐛 Troubleshooting

### Common Issues

**Issue 1: Build Errors**
```
Error: The type or namespace name 'FamilyTreeNode' could not be found
```
**Solution**: Ensure all class files are included in the project and namespaces are correct.

**Issue 2: Application Won't Start**
```
Error: .NET 6.0 not found
```
**Solution**: Install .NET 6.0 SDK from https://dotnet.microsoft.com/download

**Issue 3: Search Returns Nothing**
```
Member 'XXX' not found
```
**Solution**: 
- Check spelling (search is case-insensitive but must match exactly)
- Verify member exists in tree
- Try searching for "King Charles III" or "William" as test

**Issue 4: Cannot Add Member**
```
Error: Parent 'XXX' not found in the family tree
```
**Solution**:
- Ensure parent name is spelled correctly
- Parent must already exist in tree
- Try adding to "William" or "King Charles III" as test

**Issue 5: Tree Display Issues**
**Solution**: Click "Refresh Tree View" button to redraw

### Getting Help

If you encounter issues not covered here:
1. Check Visual Studio Error List (View → Error List)
2. Verify all files are present in project
3. Clean and rebuild solution (Build → Clean Solution, then Build → Rebuild)
4. Ensure .NET 6.0 Desktop Development workload is installed

## 🎓 Educational Value

This project demonstrates:
- ✅ Object-Oriented Programming principles
- ✅ Tree data structures
- ✅ Graph traversal algorithms (BFS/DFS)
- ✅ GUI development with WPF
- ✅ Event-driven programming
- ✅ Input validation
- ✅ Visual rendering techniques
- ✅ Software architecture

## 🚀 Future Enhancements

Potential improvements:
- [ ] Edit existing members
- [ ] Delete members from tree
- [ ] Export tree as image (PNG/SVG)
- [ ] Save/Load tree data (JSON/XML)
- [ ] Print functionality
- [ ] Spouse relationship lines
- [ ] Search filters (by date, title, etc.)
- [ ] Undo/Redo functionality
- [ ] Multiple tree support
- [ ] Historical lineage tracking

## 📄 License

This project is for educational purposes only.

## 👨‍💻 Author

Created for ICE Task 3: Family Tree Visualization Assignment

## 🙏 Acknowledgments

- House of Windsor data based on publicly available information
- Built with C# and WPF
- Uses .NET 6.0 framework

---

**Version**: 1.0  
**Last Updated**: 2025  
**Status**: ✅ Complete and Ready for Submission

For detailed testing procedures, see TESTING_GUIDE.md  
For setup instructions, see SETUP_INSTRUCTIONS.md  
For presentation guidelines, see DEMO_GUIDE.md
