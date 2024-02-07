﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BulkyBook.Models
{
	public class ApplicationUser : IdentityUser
	{
	[Required]
    public int Name { get; set; }

	public string? StreetAdress { get; set; }
	public string? City { get; set; }
	public string? State { get; set; }
	public string? PostalCode {  get; set; }



    }
}
