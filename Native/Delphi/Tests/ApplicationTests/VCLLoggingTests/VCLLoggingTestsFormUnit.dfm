object VCLLoggingTestsForm: TVCLLoggingTestsForm
  Left = 0
  Top = 0
  Caption = 'VCLLoggingTestsForm'
  ClientHeight = 453
  ClientWidth = 840
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object TopPanel: TPanel
    Left = 0
    Top = 0
    Width = 840
    Height = 41
    Align = alTop
    TabOrder = 0
    object RunButton: TButton
      Left = 8
      Top = 9
      Width = 75
      Height = 25
      Caption = 'RunButton'
      TabOrder = 0
      OnClick = RunButtonClick
    end
  end
  object LogMemo: TMemo
    Left = 0
    Top = 41
    Width = 840
    Height = 412
    Align = alClient
    Lines.Strings = (
      'LogMemo')
    ScrollBars = ssVertical
    TabOrder = 1
  end
end
