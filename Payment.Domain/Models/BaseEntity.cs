﻿namespace Payment.Domain.Models;

public abstract class BaseEntity
{
    public virtual Guid Id { get; set; }
}