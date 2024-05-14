SELECT s.*
FROM ClassSets cs, ClassEnrollments ce, Students s, Courses c
WHERE ce.ClassSetID = cs.ClassSetID
    AND ce.StudentID = s.StudentID
    AND cs.CourseID = c.CourseID
    AND c.Title = 'Computing'

-- Find all students studying computing --