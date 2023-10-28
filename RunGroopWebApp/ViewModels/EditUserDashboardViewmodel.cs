﻿namespace RunGroopWebApp.ViewModels
{
    public class EditUserDashboardViewmodel
    {
        public string Id { get; set; }
        public int ? Pace { get;set; }

        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string State { get; set; }
        public IFormFile Image { get; set; }
         
    }

}
