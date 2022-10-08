uses References_Generated, System.Windows.Forms;

begin
    var form := new Form();
    var theType := new CSharpProject.Class1();
    form.Text := theType.ToString;
    Application.Run(form);
end.
