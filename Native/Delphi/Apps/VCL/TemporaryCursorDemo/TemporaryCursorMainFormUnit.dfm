object TemporaryCursorMainForm: TTemporaryCursorMainForm
  Left = 0
  Top = 0
  Caption = 'TemporaryCursorMainForm'
  ClientHeight = 337
  ClientWidth = 635
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object TemporaryCursorClassicButton: TButton
    Left = 8
    Top = 8
    Width = 150
    Height = 25
    Caption = 'Temporary Cursor &Classic'
    TabOrder = 0
    OnClick = TemporaryCursorClassicButtonClick
  end
  object TemporaryCursorMementoButton: TButton
    Left = 8
    Top = 39
    Width = 150
    Height = 25
    Caption = 'TemporaryCursor &Memento'
    TabOrder = 1
    OnClick = TemporaryCursorMementoButtonClick
  end
end
