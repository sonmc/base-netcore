using Base.Core.Schemas;

namespace Base.App.UseCases
{
    public class GetCurrentUserPresenter
    {
        public static List<User> PresentList(List<User> items)
        {
            var result = new List<User>();
            foreach (var item in items)
            {
                var user = PresentItem(item);
                result.Add(user);
            }
            return result;
        }

        public static User PresentItem(User item)
        {
            User user = new User();
            user.Id = item.Id;
            user.UserName = item.UserName;
            user.GroupIds = item.GroupIds;
            user.Email = item.Email;
            return user;
        }
    }
}
