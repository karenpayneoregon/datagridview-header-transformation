﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WhereInParametersApp.Models;

public partial class ContactType
{
    public int ContactTypeIdentifier { get; set; }

    public string ContactTitle { get; set; }

    public virtual ICollection<Contacts> Contacts { get; set; } = new List<Contacts>();

    public virtual ICollection<Customers> Customers { get; set; } = new List<Customers>();
}