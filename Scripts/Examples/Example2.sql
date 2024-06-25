SELECT s.*
FROM ClassSets cs, ClassEnrollments ce, Students s, Courses c
WHERE ce.ClassSetId = cs.ClassSetId
    AND ce.StudentId = s.StudentId
    AND cs.CourseId = c.CourseId
    AND c.Title = 'Computing'

-- Find all students studying computing --