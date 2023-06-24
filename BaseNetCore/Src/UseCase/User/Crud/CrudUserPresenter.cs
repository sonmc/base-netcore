using BaseNetCore.Src.Services.Schemas;

namespace BaseNetCore.Src.UseCase.User.Crud
{
    public class CrudUserPresenter
    {
        public int Id { get; set; }

        public static List<UserSchema> PresentList(List<UserSchema> items)
        {
            var result = new List<UserSchema>();
            foreach (var item in items)
            {
                var user = PresentItem(item);
                result.Add(user);
            }
            return result;
        }

        public static UserSchema PresentItem(UserSchema item)
        {
            UserSchema user = new UserSchema();
            user.Id = item.Id;
            return user;
        }
    }
}
