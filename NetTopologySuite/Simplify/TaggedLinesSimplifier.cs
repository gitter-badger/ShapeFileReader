using System.Collections.Generic;

namespace NetTopologySuite.Simplify
{
    /// <summary>
    /// Simplifies a collection of <c>TaggedLineString</c>s, preserving topology
    /// (in the sense that no new intersections are introduced).
    /// This class is essentially just a container for the common
    /// indexes used by <see cref="TaggedLineStringSimplifier"/>.
    /// </summary>
    public class TaggedLinesSimplifier
    {
        private readonly LineSegmentIndex _inputIndex = new LineSegmentIndex();
        private readonly LineSegmentIndex _outputIndex = new LineSegmentIndex();

        /// <summary>
        /// Gets or sets the distance tolerance for the simplification.<br/>
        /// Points closer than this tolerance to a simplified segment may
        /// be removed.
        /// </summary>        
        public double DistanceTolerance { get; set; }

        /// <summary>
        /// Simplifies a collection of <c>TaggedLineString</c>s.
        /// </summary>
        /// <param name="taggedLines">The collection of lines to simplify.</param>
        public void Simplify(ICollection<TaggedLineString> taggedLines)
        {
            foreach (var taggedLineString in taggedLines)
                _inputIndex.Add(taggedLineString);

            foreach (var taggedLineString in taggedLines)
            {
                var tlss = new TaggedLineStringSimplifier(_inputIndex, _outputIndex);
                tlss.DistanceTolerance = DistanceTolerance;
                tlss.Simplify(taggedLineString);
            }

            /*
            for (IEnumerator i = taggedLines.GetEnumerator(); i.MoveNext(); )            
                _inputIndex.Add((TaggedLineString)i.Current);
            for (IEnumerator i = taggedLines.GetEnumerator(); i.MoveNext(); )
            {
                TaggedLineStringSimplifier tlss
                              = new TaggedLineStringSimplifier(_inputIndex, _outputIndex);
                tlss.DistanceTolerance = _distanceTolerance;
                tlss.Simplify((TaggedLineString)i.Current);
            }
             */
        }
    }
}
