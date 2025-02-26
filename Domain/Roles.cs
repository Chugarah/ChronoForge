﻿namespace Domain;

public class Roles
{
    public int Id { get; init; }
    public ICollection<Users> Users { get; init; } = new List<Users>();

    public string Name { get; init; } = null!;
}