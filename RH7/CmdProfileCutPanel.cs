namespace ProfileCut7
{
  /// Every RhinoCommon plug-in can have one or more Rhino.Commands.Command
  /// inherited classes. DO NOT create instances of this class yourself. It is the
  /// responsibility of Rhino to create an instance of this class.
  [System.Runtime.InteropServices.Guid("12F9CA6C-9ACB-41E8-B8C2-40A3FFC669F6")]
    public class CmdProfileCutPanel : Rhino.Commands.Command
  {
    public CmdProfileCutPanel()
    {
      // Rhino only creates one instance of each command class defined in a
      // plug-in, so it is safe to store a refence in a static property.
      Instance = this;
    }

    /// <summary>
    /// The only instance of this command.
    /// </summary>
    public static CmdProfileCutPanel Instance
    {
      get;
      private set;
    }

    /// <returns> 
    /// The command name as it appears on the Rhino command line.
    /// </returns>
    public override string EnglishName
    {
      get { return "ProfileCutPanel"; }
    }

    protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
    {
      System.Guid panelId = ProfileCutPanel.PanelId;
      bool bVisible = Rhino.UI.Panels.IsPanelVisible(panelId);

      string prompt = (bVisible)
        ? "ProfileCutPanel is visible. New value"
        : "ProfileCutPanel is hidden. New value";

      Rhino.Input.Custom.GetOption go = new Rhino.Input.Custom.GetOption();
      int hide_index = go.AddOption("Hide");
      int show_index = go.AddOption("Show");
      int toggle_index = go.AddOption("Toggle");

      go.Get();
      if (go.CommandResult() != Rhino.Commands.Result.Success)
        return go.CommandResult();

      Rhino.Input.Custom.CommandLineOption option = go.Option();
      if (null == option)
        return Rhino.Commands.Result.Failure;

      int index = option.Index;

      if (index == hide_index)
      {
        if (bVisible)
          Rhino.UI.Panels.ClosePanel(panelId);
      }
      else if (index == show_index)
      {
        if (!bVisible)
          Rhino.UI.Panels.OpenPanel(panelId);
      }
      else if (index == toggle_index)
      {
        if (bVisible)
          Rhino.UI.Panels.ClosePanel(panelId);
        else
          Rhino.UI.Panels.OpenPanel(panelId);
      }

      return Rhino.Commands.Result.Success;
    }
  }
}
