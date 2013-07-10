program Message_Directive;

{$APPTYPE CONSOLE}

{$R *.res}

begin
  {$MESSAGE 'Boo!'}
  {$Message Hint 'Feed the cats'}
  {$messaGe Warn 'Looks like rain.'}
  {$Message Error 'Not implemented'}
  {$Message Fatal 'Bang.  Yer dead.'}
{
[DCC Hint] Message_Directive.dpr(8): H1054 Boo!
[DCC Hint] Message_Directive.dpr(9): H1054 Feed the cats
[DCC Warning] Message_Directive.dpr(10): W1054 Looks like rain.
[DCC Error] Message_Directive.dpr(11): E1054 Not implemented
[DCC Fatal Error] Message_Directive.dpr(12): F1054 Bang.  Yer dead.
}
end.
