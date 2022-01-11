[User.cs]
[Account.cs]

[Account] inherits [User]

User:
public virtual string Username

Account:
public override string Username
    get: return base.Username
    set: base.Username = value

Account is Database Object
User is Application Object

