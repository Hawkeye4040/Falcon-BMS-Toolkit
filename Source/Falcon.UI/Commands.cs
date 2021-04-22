using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace Falcon.UI
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class ATOCommands
    {
        #region Fields

        public static readonly RoutedUICommand ViewPackage = new RoutedUICommand
        (
            "View Package",
            "ViewPackageCommand",
            typeof(ATOCommands)
        );

        public static readonly RoutedUICommand ViewFlight = new RoutedUICommand
        (
            "View Flight",
            "ViewFlightCommand",
            typeof(ATOCommands)
        );

        public static readonly RoutedUICommand ViewDTC = new RoutedUICommand("View DTC", "ViewDTCCommand", typeof(ATOCommands));

        public static readonly RoutedUICommand ViewSquadron = new RoutedUICommand("View Squadron", "ViewSquadronCommand", typeof(ATOCommands));

        public static readonly RoutedUICommand ViewLoadout = new RoutedUICommand("View Loadout", "ViewLoadoutCommand", typeof(ATOCommands));

        public static readonly RoutedUICommand ViewFlightPlan = new RoutedUICommand("View Flight Plan", "ViewFlightPlanCommand", typeof(ATOCommands));

        #endregion
    }

    public static class BriefingCommands
    {
        
    }

    public static class CommonCommands
    {
        #region Fields

        public static readonly RoutedUICommand Confirm = new RoutedUICommand
        (
            "Confirm",
            "ConfirmCommand",
            typeof(CommonCommands)
        );

        public static readonly RoutedUICommand Cancel = new RoutedUICommand
        (
            "Cancel",
            "CancelCommand",
            typeof(CommonCommands)
        );

        public static readonly RoutedUICommand Fullscreen = new RoutedUICommand
        (
            "Fullscreen",
            "FullscreenCommand",
            typeof(CommonCommands)
        );

        #endregion
    }

    public static class MenuCommands
    {
        #region Fields

        // File Menu

        public static readonly RoutedUICommand NewCampaign = new RoutedUICommand
        (
            "New Campaign",
            "NewCampaignCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand NewTacticalEngagement = new RoutedUICommand
        (
            "New Tactical Engagement",
            "NewTacticalEngagementCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand NewKneeboard = new RoutedUICommand
        (
            "New KneeboardEditor",
            "NewKneeboardCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand NewMissionPlan = new RoutedUICommand
        (
            "New Mission Plan",
            "NewMissionPlanCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand NewTheater = new RoutedUICommand
        (
            "New Theater",
            "NewTheaterCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand OpenCampaign = new RoutedUICommand
        (
            "Open Campaign",
            "OpenCampaignCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand OpenTacticalEngagement = new RoutedUICommand
        (
            "Open Tactical Engagement",
            "OpenTacticalEngagementCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand OpenTheater = new RoutedUICommand
        (
            "Open Theater",
            "OpenTheaterCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand OpenMissionPlan = new RoutedUICommand
        (
            "Open Mission Plan",
            "OpenMissionPlanCommand",
            typeof(MenuCommands)
        );

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static readonly RoutedUICommand OpenDTC = new RoutedUICommand
        (
            "Open Data Cartridge (DTC)",
            "OpenDTCCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand Save = new RoutedUICommand
        (
            "Save",
            "SaveCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<KeyGesture> {new KeyGesture(Key.S, ModifierKeys.Control)})
        );

        public static readonly RoutedUICommand SaveAs = new RoutedUICommand
        (
            "Save As",
            "SaveAsCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand SaveAll = new RoutedUICommand
        (
            "Save All",
            "SaveAllCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<KeyGesture>
                {new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)})
        );

        public static readonly RoutedUICommand ManageConnections = new RoutedUICommand
        (
            "Manage Connections",
            "MangeConnectionsCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand Settings = new RoutedUICommand
        (
            "Settings",
            "SettingsCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand CloseWindow = new RoutedUICommand
        (
            "Close",
            "CloseWindowCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.X, ModifierKeys.Alt)})
        );

        public static readonly RoutedUICommand Close = new RoutedUICommand
        (
            "Close",
            "CloseCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand Exit = new RoutedUICommand
        (
            "Exit",
            "ExitCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.F4, ModifierKeys.Alt)})
        );

        // Edit Menu

        public static readonly RoutedUICommand Undo = new RoutedUICommand
        (
            "Undo",
            "UndoCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.Z, ModifierKeys.Control)})
        );

        public static readonly RoutedUICommand Redo = new RoutedUICommand
        (
            "Redo",
            "RedoCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.Y, ModifierKeys.Control)})
        );

        public static readonly RoutedUICommand Cut = new RoutedUICommand
        (
            "Cut",
            "CutCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.X, ModifierKeys.Control)})
        );

        public static readonly RoutedUICommand Copy = new RoutedUICommand
        (
            "Copy",
            "CopyCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.C, ModifierKeys.Control)})
        );

        public static readonly RoutedUICommand Paste = new RoutedUICommand
        (
            "Paste",
            "PasteCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.V, ModifierKeys.Control)})
        );

        public static readonly RoutedUICommand Delete = new RoutedUICommand
        (
            "Delete",
            "DeleteCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.Delete)})
        );

        // View Menu

        public static readonly RoutedUICommand KneeboardEditor = new RoutedUICommand
        (
            "KneeboardEditor",
            "ViewKneeboardCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand DataCartridgeEditor = new RoutedUICommand
        (
            "Data Cartridge",
            "DataCartridgeEditor",
            typeof(MenuCommands)
        );

        // Build Menu

        public static readonly RoutedUICommand Build = new RoutedUICommand
        (
            "Build",
            "BuildCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<InputGesture> {new KeyGesture(Key.F5)})
        );

        // Help Menu

        public static readonly RoutedUICommand Documentation = new RoutedUICommand
        (
            "Documentation",
            "DocumentationCommand",
            typeof(MenuCommands),
            new InputGestureCollection(new List<KeyGesture> {new KeyGesture(Key.F1)})
        );

        public static readonly RoutedUICommand Update = new RoutedUICommand
        (
            "Check for Updates",
            "UpdateCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand Feedback = new RoutedUICommand
        (
            "Send Feedback",
            "FeedbackCommand",
            typeof(MenuCommands)
        );

        public static readonly RoutedUICommand About = new RoutedUICommand
        (
            "About",
            "AboutCommand",
            typeof(MenuCommands)
        );

        #endregion
    }
}