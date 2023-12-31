﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftBibliotek.Models
{
    internal class Book
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string ISBN { get; set; }
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public int PublicationYear { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public int AmountOfTimesBorrowed { get; set; }
        public ICollection<LoanHistory> LoanHistory { get; set; } = new List<LoanHistory>();
        public DateTime? DateOfLoan { get; set; }
        public DateTime? PlannedDateOfReturn { get; set; }
        public override string ToString()
        {
            return Title;
        }
        public Book()
        {
            
        }
        public Book(Book copy)
        {
            Title = copy.Title;
            ISBN = copy.ISBN;
            Rating = copy.Rating;
            Authors = copy.Authors;
            PublicationYear = copy.PublicationYear;
            AmountOfTimesBorrowed = copy.AmountOfTimesBorrowed;
            LoanHistory = copy.LoanHistory;
        }
    }
}