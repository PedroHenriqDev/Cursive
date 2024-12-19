﻿using System.IO.Pipes;
using Cursive.Domain.Entities.Abstractions;
using Cursive.Domain.Utils;
using Cursive.Domain.Validations;
using Cursive.Domain.ValueObjects;

namespace Cursive.Domain.Entities
{
    public class User : Entity
    {
        private User()
        {
        }

        public User(Name name, string email, string password) : base(GuidUtils.GenerateId(), DateTime.Now)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public Name Name { get; private set; } = new Name();
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public DateTime BirthDate { get; private set; }


        public override Validation Validate()
        {
            IList<ValidationMessage> validationMessages = new List<ValidationMessage>();

            if (string.IsNullOrEmpty(Name.FirstName) || string.IsNullOrEmpty(Name.LastName))
                validationMessages.Add(new ValidationMessage(nameof(Name), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(Name))));

            if (string.IsNullOrEmpty(Email))
                validationMessages.Add(new ValidationMessage(nameof(Email), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(Email))));

            if(BirthDate == DateTime.MinValue || BirthDate > DateTime.Now)
                validationMessages.Add(new ValidationMessage(nameof(Email), Messages.GREATER_DATE));

            return new Validation(!validationMessages.Any(), validationMessages);
        }
    }
}