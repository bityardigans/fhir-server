﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using EnsureThat;
using Hl7.Fhir.Model;
using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;

namespace Microsoft.Health.Fhir.Core.Features.Search.Converters
{
    /// <summary>
    /// A converter used to convert from <see cref="ResourceReference"/> to a list of <see cref="ISearchValue"/>.
    /// </summary>
    public class ResourceReferenceToSearchValueTypeConverter : FhirElementToSearchValueTypeConverter<ResourceReference>
    {
        private readonly IReferenceSearchValueParser _referenceSearchValueParser;

        public ResourceReferenceToSearchValueTypeConverter(IReferenceSearchValueParser referenceSearchValueParser)
        {
            EnsureArg.IsNotNull(referenceSearchValueParser, nameof(referenceSearchValueParser));

            _referenceSearchValueParser = referenceSearchValueParser;
        }

        protected override IEnumerable<ISearchValue> ConvertTo(ResourceReference value)
        {
            if (value.Reference == null)
            {
                yield break;
            }

            // Contained resources will not be searchable.
            if (value.IsContainedReference)
            {
                yield break;
            }

            yield return _referenceSearchValueParser.Parse(value.Reference);
        }
    }
}
