﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MidStateShuttleService.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Must be 10 digits")]
        public long StudentId { get; set; } // Changed from int to long

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Trip Type is required")]
        public string TripType { get; set; }

        [Required(ErrorMessage = "Pick Up Location is required")]
        public string PickUpLocation { get; set; }

        [Required(ErrorMessage = "Drop Off Location is required")]
        public string DropOffLocation { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public DateTime Time { get; set; }

        // If you decide to enforce validation for SpecialRequest in the future
        // [StringLength(5, ErrorMessage = "Special Request cannot exceed 5 characters")]
        public string SpecialRequest { get; set; }

    }

}