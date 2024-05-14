SELECT s.*
FROM ClassSets cs, ClassEnrollments ce, Students s
WHERE ce.ClassSetID = cs.ClassSetID
    AND ce.StudentID = s.StudentID
    AND cs.ClassSetID = 1

-- Find all students in class 1 --