using EventBase;

namespace DiagnoseEvent
{
    public class Diagnose : BaseEvent
    {
        public Diagnose()
            :base()
        {
            Multiples.Add(2, DiagnoseLibConstants.Diagnose);
            Multiples.Add(7, DiagnoseLibConstants.Patient);
        }
        public override string Name()
        {
            return DiagnoseLibConstants.Name;
        }
    }
}
