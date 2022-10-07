namespace ZimLabs.Mapper
{
    /// <summary>
    /// Represents a change entry
    /// </summary>
    public class ChangeEntry
    {
        /// <summary>
        /// Gets the name of the property
        /// </summary>
        public string Property { get; }

        /// <summary>
        /// Gets the old value
        /// </summary>
        public string OldValue { get; }

        /// <summary>
        /// Gets the new value
        /// </summary>
        public string NewValue { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ChangeEntry"/>
        /// </summary>
        /// <param name="property">The name of the property</param>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        public ChangeEntry(string property, object oldValue, object newValue)
        {
            Property = property;
            OldValue = oldValue?.ToString() ?? "";
            NewValue = newValue?.ToString() ?? "";
        }
    }
}
