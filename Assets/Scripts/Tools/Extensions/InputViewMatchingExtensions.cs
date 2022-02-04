using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class InputViewMatchingExtensions
    {
        public static bool IsHaveDuplicatesInInputTypes(this List<InputViewMatching> self)
        {
            var groupInputTypes = self.GroupBy(element => element.InputType).Select(s => new { s.Key, DuplicateCount = s.Count() }).OrderBy(g => g.Key);

            foreach (var element in groupInputTypes)
            {
                if (element.DuplicateCount > 1) return true;
            }

            return false;
        }
    }
}