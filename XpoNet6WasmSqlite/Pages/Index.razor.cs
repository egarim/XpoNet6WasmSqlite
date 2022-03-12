using DevExpress.Xpo;
using Microsoft.AspNetCore.Components;

namespace XpoNet6WasmSqlite.Pages
{
    public partial class Index
    {
        [Inject]
        private UnitOfWork UoW { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        private void Save()
        {
            User user = new User(this.UoW);
            user.Name = this.Name;
            UoW.CommitChanges();
            this.Users.Add(user);
        }
        
        protected override async Task OnInitializedAsync()
        {
            Users = this.UoW.Query<User>().ToList();
        }
    }
}
