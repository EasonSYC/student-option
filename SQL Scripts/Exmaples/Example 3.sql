SELECT cs.*
FROM Classes c, ClassEnrollments ce, Students s, Courses cs
WHERE ce.ClassID = c.ClassID
AND ce.StudentID = s.StudentID
AND c.CourseID = cs.CourseID
AND s.StudentID = 1

-- Find all courses taken by student 1 --