using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

IUserRepository userRepository = new UserRepository();

#region Create User Dialog

var userEntity = new UserEntity();

Console.Write("First name: ");
userEntity.FirstName = Console.ReadLine()!;
Console.Write("Last name: ");
userEntity.LastName = Console.ReadLine()!;
Console.Write("Email: ");
userEntity.Email = Console.ReadLine()!;
Console.Write("Phone number: ");
userEntity.PhoneNumber = Console.ReadLine()!;

var result = userRepository.Create(userEntity);
if (result)
{
    Console.WriteLine("User created successfully.");
}
else
{
    Console.WriteLine("Failed to create user.");
}
Console.ReadKey();

#endregion

var users = userRepository.GetAll();

Console.Clear();

foreach (var user in users)
{
    Console.WriteLine($"#{user.Id}, {user.FirstName} {user.LastName} <{user.Email}> {user.PhoneNumber}");
    // Visual Studio förslog detta också som entity: {user.CreatedAt} ska fixa någon gång för den visar fel för jag la till entity med datetime.
    Console.WriteLine("");
}