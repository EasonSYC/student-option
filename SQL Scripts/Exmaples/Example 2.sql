SELECT s.*
FROM Classes c, ClassEnrollments ce, Students s, Courses cs
WHERE ce.ClassID = c.ClassID
AND ce.StudentID = s.StudentID
AND c.CourseID = cs.CourseID
AND cs.Title = 'Computing'

-- Find all students studying computing --