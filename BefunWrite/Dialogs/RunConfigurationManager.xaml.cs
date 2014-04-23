using BefunGen.AST.CodeGen;
using System.Windows;

namespace BefunWrite.Dialogs
{
	/// <summary>
	/// Interaction logic for RunConfigurationManager.xaml
	/// </summary>
	public partial class RunConfigurationManager : Window
	{
		private TextFungeProjectWrapper project;

		public RunConfigurationManager(TextFungeProjectWrapper wrapper)
		{
			InitializeComponent();

			project = wrapper;

			UpdateListBox();
			cbxConfigs.SelectedIndex = wrapper.ProjectConfig.SelectedConfiguration;
		}

		#region Events

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			SaveContent();

			UpdateListBox();
		}

		private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			UpdateContent();
		}

		private void New_Click(object sender, RoutedEventArgs e)
		{
			project.ProjectConfig.Configurations.Add(new ProjectCodeGenOptions() { Name = "<Config>", Options = CodeGenOptions.getCGO_Release() });

			project.DirtyProject();

			UpdateListBox();
			cbxConfigs.SelectedIndex = project.ProjectConfig.Configurations.Count - 1;
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (project.ProjectConfig.Configurations.Count <= 1)
				return;

			int si = cbxConfigs.SelectedIndex;

			if (si >= 0)
			{
				project.ProjectConfig.Configurations.RemoveAt(si);
			}

			project.DirtyProject();

			UpdateListBox();

			cbxConfigs.SelectedIndex = 0;
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		#endregion

		private void UpdateListBox()
		{
			int cbsi = cbxConfigs.SelectedIndex;

			cbxConfigs.Items.Clear();

			project.ProjectConfig.Configurations.ForEach(p => cbxConfigs.Items.Add(p));

			cbxConfigs.SelectedIndex = cbsi;
		}

		private bool UpdateContent()
		{
			int si = cbxConfigs.SelectedIndex;


			if (si < 0 || si >= project.ProjectConfig.Configurations.Count)
			{
				return false;
			}

			ProjectCodeGenOptions o = project.ProjectConfig.Configurations[si];

			V_ConfigName.Text = o.Name;

			//########

			V_EX_IsDebug.IsChecked = o.ExecSettings.IsDebug;
			V_EX_StartPaused.IsChecked = o.ExecSettings.startPaused;
			V_EX_SyntaxHighlighting.IsChecked = o.ExecSettings.syntaxHighlight;
			V_EX_ASCIIStack.IsChecked = o.ExecSettings.asciistack;
			V_EX_SkipNOP.IsChecked = o.ExecSettings.skipnop;
			V_EX_InitialSpeedIndex.Value = o.ExecSettings.initialSpeedIndex;
			V_EX_Speed_1.Value = o.ExecSettings.simuSpeeds[0];
			V_EX_Speed_2.Value = o.ExecSettings.simuSpeeds[1];
			V_EX_Speed_3.Value = o.ExecSettings.simuSpeeds[2];
			V_EX_Speed_4.Value = o.ExecSettings.simuSpeeds[3];
			V_EX_Speed_5.Value = o.ExecSettings.simuSpeeds[4];
			V_EX_DoDecay.IsChecked = o.ExecSettings.dodecay;
			V_EX_DecayTime.Value = o.ExecSettings.decaytime;
			V_EX_ZoomToDisplay.IsChecked = o.ExecSettings.zoomToDisplay;

			//########

			V_NumberLiteralRepresentation.SelectedIndex = CodeGenOptions.NumberRepToUINumber(o.Options.NumberLiteralRepresentation);

			V_StripDoubleStringmodeToogle.IsChecked = o.Options.StripDoubleStringmodeToogle;

			V_SetNOPCellsToCustom.IsChecked = o.Options.SetNOPCellsToCustom;
			V_CustomNOPSymbol.CValue = o.Options.CustomNOPSymbol;

			V_CompressHorizontalCombining.IsChecked = o.Options.CompressHorizontalCombining;
			V_CompressVerticalCombining.IsChecked = o.Options.CompressVerticalCombining;

			V_DefaultVarDeclarationWidth.Value = o.Options.DefaultVarDeclarationWidth;

			V_DefaultVarDeclarationSymbol.CValue = o.Options.DefaultVarDeclarationSymbol.getCommandCode();
			V_DefaultTempSymbol.CValue = o.Options.DefaultTempSymbol.getCommandCode();
			V_DefaultResultTempSymbol.CValue = o.Options.DefaultResultTempSymbol.getCommandCode();

			V_ExtendedBooleanCast.IsChecked = o.Options.ExtendedBooleanCast;

			V_DefaultNumeralValue.Value = o.Options.DefaultNumeralValue;
			V_DefaultCharacterValue.CValue = o.Options.DefaultCharacterValue;
			V_DefaultBooleanValue.IsChecked = o.Options.DefaultBooleanValue;

			V_DefaultDisplayValue.CValue = o.Options.DefaultDisplayValue.getCommandCode();
			V_DisplayBorder.CValue = o.Options.DisplayBorder.getCommandCode();
			V_DisplayBorderThickness.Value = o.Options.DisplayBorderThickness;

			V_DisplayModuloAccess.IsChecked = o.Options.DisplayModuloAccess;

			V_CompileTimeEvaluateExpressions.IsChecked = o.Options.CompileTimeEvaluateExpressions;

			return true;
		}

		private bool SaveContent()
		{
			int si = cbxConfigs.SelectedIndex;


			if (si < 0 || si >= project.ProjectConfig.Configurations.Count)
			{
				return false;
			}

			ProjectCodeGenOptions o = project.ProjectConfig.Configurations[si];

			o.Name = V_ConfigName.Text;

			//########

			o.ExecSettings.IsDebug = V_EX_IsDebug.IsChecked.Value;
			o.ExecSettings.startPaused = V_EX_StartPaused.IsChecked.Value;
			o.ExecSettings.syntaxHighlight = V_EX_SyntaxHighlighting.IsChecked.Value;
			o.ExecSettings.asciistack = V_EX_ASCIIStack.IsChecked.Value;
			o.ExecSettings.skipnop = V_EX_SkipNOP.IsChecked.Value;
			o.ExecSettings.initialSpeedIndex = V_EX_InitialSpeedIndex.Value.Value;
			o.ExecSettings.simuSpeeds[0] = V_EX_Speed_1.Value.Value;
			o.ExecSettings.simuSpeeds[1] = V_EX_Speed_2.Value.Value;
			o.ExecSettings.simuSpeeds[2] = V_EX_Speed_3.Value.Value;
			o.ExecSettings.simuSpeeds[3] = V_EX_Speed_4.Value.Value;
			o.ExecSettings.simuSpeeds[4] = V_EX_Speed_5.Value.Value;
			o.ExecSettings.dodecay = V_EX_DoDecay.IsChecked.Value;
			o.ExecSettings.decaytime = V_EX_DecayTime.Value.Value;
			o.ExecSettings.zoomToDisplay = V_EX_ZoomToDisplay.IsChecked.Value;

			//########

			o.Options.NumberLiteralRepresentation = CodeGenOptions.UINumberToNumberRep(V_NumberLiteralRepresentation.SelectedIndex, NumberRep.Best);

			o.Options.StripDoubleStringmodeToogle = V_StripDoubleStringmodeToogle.IsChecked.Value;

			o.Options.SetNOPCellsToCustom = V_SetNOPCellsToCustom.IsChecked.Value;
			o.Options.CustomNOPSymbol = V_CustomNOPSymbol.CValue;

			o.Options.CompressHorizontalCombining = V_CompressHorizontalCombining.IsChecked.Value;
			o.Options.CompressVerticalCombining = V_CompressVerticalCombining.IsChecked.Value;

			o.Options.DefaultVarDeclarationWidth = V_DefaultVarDeclarationWidth.Value.Value;

			o.Options.DefaultVarDeclarationSymbol = BCHelper.chr(V_DefaultVarDeclarationSymbol.CValue);
			o.Options.DefaultTempSymbol = BCHelper.chr(V_DefaultTempSymbol.CValue);
			o.Options.DefaultResultTempSymbol = BCHelper.chr(V_DefaultResultTempSymbol.CValue);

			o.Options.ExtendedBooleanCast = V_ExtendedBooleanCast.IsChecked.Value;

			o.Options.DefaultNumeralValue = (byte)V_DefaultNumeralValue.Value.Value;
			o.Options.DefaultCharacterValue = V_DefaultCharacterValue.CValue;
			o.Options.DefaultBooleanValue = V_DefaultBooleanValue.IsChecked.Value;

			o.Options.DefaultDisplayValue = BCHelper.chr(V_DefaultDisplayValue.CValue);
			o.Options.DisplayBorder = BCHelper.chr(V_DisplayBorder.CValue);
			o.Options.DisplayBorderThickness = V_DisplayBorderThickness.Value.Value;

			o.Options.DisplayModuloAccess = V_DisplayModuloAccess.IsChecked.Value;

			o.Options.CompileTimeEvaluateExpressions = V_CompileTimeEvaluateExpressions.IsChecked.Value;

			project.DirtyProject();

			return true;
		}
	}
}
