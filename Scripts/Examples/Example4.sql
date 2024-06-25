SELECT cs.*
FROM ExamBoards eb, Courses cs
WHERE eb.ExamBoardId = cs.ExamBoardId
    AND eb.ExamBoardName = 'AQA'

-- Find all AQA Courses --