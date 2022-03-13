using Blazored.LocalStorage;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;

namespace XpoNet6WasmSqlite.Pages
{
    public partial class Index
    {
       
        [Inject]
        private IJSRuntime js { get; set; }

        [Inject]
        private ILocalStorageService localStorage { get; set; }

        //[Inject]
        private UnitOfWork UoW { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        private void Save()
        {
            var test=File.Exists("mydb.db");
            Debug.WriteLine($"test{test}");
            User user = new User(this.UoW);
            user.Name = this.Name;
            UoW.CommitChanges();
            this.Users.Add(user);
            this.Name = "";
            SaveDatabaseToLocalStorage();
        }
        async void  DownloadFile()
        {
           
            this.UoW.Disconnect();
            var DbBytes=File.ReadAllBytes("mydb.db");
            await FileUtil.SaveAs(js, "mydb.db", DbBytes);
        }
        async void SaveDatabaseToLocalStorage()
        {
            //this.UoW.Disconnect();
            var DbBytes = File.ReadAllBytes("mydb.db");
            await localStorage.SetItemAsync<byte[]>("mydb.db", DbBytes);
        }
        protected override async Task OnInitializedAsync()
        {
            var db= await localStorage.GetItemAsync<byte[]>("mydb.db");
            if(db==null)
            {
                File.WriteAllBytes("mydb.db", GetResource("mydb.db"));
            }
            else
            {
                File.WriteAllBytes("mydb.db", db);
            }
            InitXpo();
            Users = this.UoW.Query<User>().ToList();
        }
        void InitXpo()
        {
            //best practice #7
            //https://supportcenter.devexpress.com/ticket/details/a2944/xpo-best-practices
            IDataLayer dl = XpoDefault.GetDataLayer("XpoProvider=SQLite;Data Source=mydb.db", DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            using (Session session = new Session(dl))
            {
                System.Reflection.Assembly[] assemblies = new System.Reflection.Assembly[] {
                typeof(User).Assembly };
       
                session.UpdateSchema(assemblies);
                session.CreateObjectTypeRecords(assemblies);
            }
            this.UoW=new UnitOfWork(dl);
        }
        public byte[] GetResource(string filename)
        {
            string result = string.Empty;

            using (var stream = this.GetType().Assembly.
                       GetManifestResourceStream("XpoNet6WasmSqlite." + filename))
            {
                var Ms = new MemoryStream();
                stream.CopyTo(Ms);
               return Ms.ToArray();
            }
           
        }
    }
}
