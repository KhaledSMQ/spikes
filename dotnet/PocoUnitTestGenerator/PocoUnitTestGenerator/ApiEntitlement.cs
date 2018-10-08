using System;

namespace SpikesCo.Platform.Entitlements.Entities
{
	public class ApiEntitlement
	{
		public string UserId { get; set; }
        public int? CompanyId { get; set; }
        /*public string Application { get; set; }
		public string Service { get; set; }
		public string Controller { get; set; }
        public string Action { get; set; }
        public DateTimeOffset BeginDate { get; set; }*/
        public DateTimeOffset EndDate { get; set; }
        public bool IsAllowed { get; set; }
	    public ICollection<int> SomeInts { get; set; }
	    public IEnumerable<AbcD> Abcds { get; set; }
        public AbcD Efgh { get; set; }
        public Abc Def { get; }
        public Ghi Jkl { set; }
        public Mno Pqr { get {  } set {  } }
	    public Stu Vwx { get { return 0; } set { var dummy = value; } }
	}
}
