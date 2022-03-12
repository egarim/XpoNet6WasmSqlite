using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XpoNet6WasmSqlite
{
    public class User:DevExpress.Xpo.XPObject
    {
        protected User()
        {

        }

        public User(Session session) : base(session)
        {

        }

        protected User(Session session, XPClassInfo classInfo) : base(session, classInfo)
        {

        }

        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
    }
}
