﻿/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Runtime.Serialization;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents errors that occur during the encoding of a new <see cref="IAudioFile"/>.
    /// </summary>
    /// <seealso cref="AudioException"/>
    [Serializable]
    public sealed class AudioEncodingException : AudioException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncodingException"/> class.
        /// </summary>
        public AudioEncodingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncodingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AudioEncodingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncodingException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AudioEncodingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioEncodingException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the
        /// exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the
        /// source or destination.</param>
        AudioEncodingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
