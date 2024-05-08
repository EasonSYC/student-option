SELECT s.*
FROM Classes c, ClassEnrollments ce, Students s
WHERE ce.ClassID = c.ClassID
AND ce.StudentID = s.StudentID
AND c.ClassID = 1

-- Find all students in class 1 --