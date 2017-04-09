using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime DateTime => DateTime.Parse($"{Date} {Time}");

        public string Heading { get; set; }

        public string Action
        {
            //(Id != 0) ? "Update" : "Create";
            get
            {
                Expression<Func<GigsController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<GigsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        // this is like get {return DateTime.Parse($"{Date} {Time}")}
    }
}