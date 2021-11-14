namespace Soot.Dotnet.TestSnippets.Types
{
    public class Inherit
    {
        public static void inh()
        {
            A a = new C();
            C c = (C) a;

            B bb = new B();
            bb.Overr();
            bb.Hide();
            
        }
    }

    public class A
    {
        public virtual void Overr() { }
        public void Hide() { }
    }
        
    public class B : A
    {
        public override void Overr() { }
        public new void Hide() { }
    }
        
    public sealed class C : B
    {
        public int I { get; set; }
    }
}