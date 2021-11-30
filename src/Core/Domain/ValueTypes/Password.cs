﻿using Domain.Abstractions.ValueTypes;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Domain.ValueTypes;

public struct Password : IValueType
{
    private readonly string _password;
    private readonly List<ValidationFailure> _errors;

    private Password(string password)
        : this()
    {
        _password = password;
        _errors = new List<ValidationFailure>();
        Validate();
    }

    public bool IsValid => Errors?.Any() is false;

    public IReadOnlyCollection<ValidationFailure> Errors { get; private set; }

    public override string ToString() =>
        _password;

    public static implicit operator Password(string password) =>
        new Password(password);

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_password))
            _errors.Add(new ValidationFailure(GetType().Name, "Is necessary to inform the Password."));

        if (_password.Length < 6)
            _errors.Add(new ValidationFailure(GetType().Name, "The password must have more than 6 chars."));

        if (Regex.IsMatch(_password, (@"[^a-zA-Z0-9]")))
            _errors.Add(new ValidationFailure(GetType().Name, "The password must not have any special char."));

        Errors = _errors;
    }
}