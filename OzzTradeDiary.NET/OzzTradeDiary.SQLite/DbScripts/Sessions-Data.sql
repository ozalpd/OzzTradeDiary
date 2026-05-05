INSERT INTO Sessions
(ExchangeId, SessionType, LocalOpen, LocalClose, DayOfWeek) VALUES
-- 🇺🇸 NYSE (Id = 1)
     (1, 1, '09:30', '16:00', NULL),  -- Regular
     (1, 2, '04:00', '09:30', NULL),  -- PreMarket
     (1, 3, '16:00', '20:00', NULL),  -- AfterHours
-- 🇺🇸 NASDAQ (Id = 2)
     (2, 1, '09:30', '16:00', NULL),
     (2, 2, '04:00', '09:30', NULL),
     (2, 3, '16:00', '20:00', NULL),
-- 🇺🇸 CME (Id = 3)
     (3, 0, '16:00', '17:00', NULL), -- Maintenance break
     (3, 8, '08:30', '15:15', NULL), -- Regular Trading Hours      (RTH)
-- Electronic Trading Hours      (ETH)
     (3, 9, '17:00', '23:59', 0),    -- Sunday
     (3, 9, '00:00', '16:00', 1),    -- Mon
     (3, 9, '00:00', '16:00', 2),    -- Tue
     (3, 9, '00:00', '16:00', 3),    -- Wed
     (3, 9, '00:00', '16:00', 4),    -- Thu
     (3, 9, '00:00', '16:00', 5),    -- Fri
-- 🇹🇷 BIST (Id = 4)
     (4, 1, '10:00', '18:00', NULL),
-- 🇬🇧 LSE (Id = 10)
     (10, 1, '08:00', '16:30', NULL),
     (10, 2, '05:05', '07:50', NULL),
     (10, 3, '16:40', '17:15', NULL),
-- 🇯🇵 TSE (Id = 11)
     (11, 1, '09:00', '11:30', NULL),  -- Morning
     (11, 0, '11:30', '12:30', NULL),  -- Lunch break
     (11, 1, '12:30', '15:00', NULL),  -- Afternoon
-- 🇭🇰 HKEX (Id = 12)
     (12, 1, '09:30', '12:00', NULL),  -- Morning
     (12, 0, '12:00', '13:00', NULL),  -- Lunch break
     (12, 1, '13:00', '16:00', NULL),  -- Afternoon
-- 🇸🇬 SGX (Id = 14)
     (14, 1, '09:00', '12:00', NULL),
     (14, 0, '12:00', '13:00', NULL),
     (14, 1, '13:00', '17:00', NULL),
-- 🇨🇳 SSE (Id = 15)
     (15, 1, '09:30', '11:30', NULL),
     (15, 0, '11:30', '13:00', NULL),
     (15, 1, '13:00', '15:00', NULL),
-- 🇨🇳 SZSE (Id = 16)
     (16, 1, '09:30', '11:30', NULL),
     (16, 0, '11:30', '13:00', NULL),
     (16, 1, '13:00', '15:00', NULL),
-- 🇰🇷 KRX (Id = 17)
     (17, 1, '09:00', '15:30', NULL)

