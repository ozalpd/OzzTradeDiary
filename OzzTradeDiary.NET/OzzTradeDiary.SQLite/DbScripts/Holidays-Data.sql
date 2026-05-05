-- Fixed-date BIST holidays
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
       ('National Sovereignty and Children''s Day', 4, 4, 23, NULL, 0),
       ('Labor and Solidarity Day', 4, 5, 1, NULL, 0),
       ('Commemoration of Atatürk, Youth and Sports Day', 4, 5, 19, NULL, 0),
       ('Democracy and National Unity Day', 4, 7, 15, NULL, 0),
       ('Victory Day', 4, 8, 30, NULL, 0),
       ('Republic Day', 4, 10, 29, NULL, 0);

-- US FIXED HOLIDAYS (NYSE, NASDAQ, AMEX, CBOE)
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('New Year''s Day', 1, 1, 1, NULL, 0),
         ('New Year''s Day', 2, 1, 1, NULL, 0),
         ('New Year''s Day', 8, 1, 1, NULL, 0),
         ('New Year''s Day', 9, 1, 1, NULL, 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Juneteenth National Independence Day', 1, 6, 19, NULL, 0),
         ('Juneteenth National Independence Day', 2, 6, 19, NULL, 0),
         ('Juneteenth National Independence Day', 8, 6, 19, NULL, 0),
         ('Juneteenth National Independence Day', 9, 6, 19, NULL, 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Independence Day', 1, 7, 4, NULL, 0),
         ('Independence Day', 2, 7, 4, NULL, 0),
         ('Independence Day', 8, 7, 4, NULL, 0),
         ('Independence Day', 9, 7, 4, NULL, 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Christmas Day', 1, 12, 25, NULL, 0),
         ('Christmas Day', 2, 12, 25, NULL, 0),
         ('Christmas Day', 8, 12, 25, NULL, 0),
         ('Christmas Day', 9, 12, 25, NULL, 0);

-- CME FIXED HOLIDAYS
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('New Year''s Day', 3, 1, 1, NULL, 0),
         ('Independence Day', 3, 7, 4, NULL, 0),
         ('Christmas Day', 3, 12, 25, NULL, 0);

-- LSE FIXED HOLIDAYS
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Christmas Day', 10, 12, 25, NULL, 0),
         ('Boxing Day', 10, 12, 26, NULL, 0);


-- Ramadan Feast 2024
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2024-04-09', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2024-04-10', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2024-04-11', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2024-04-12', 0);
-- Eid al-Adha 2024
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2024-06-15', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2024-06-16', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2024-06-17', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2024-06-18', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2024-06-19', 0);
-- Ramadan Feast 2025
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2025-03-30', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2025-03-31', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2025-04-01', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2025-04-02', 0);
-- Eid al-Adha 2025
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2025-06-06', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2025-06-07', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2025-06-08', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2025-06-09', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2025-06-10', 0);
-- Ramadan Feast 2026
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2026-03-19', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2026-03-20', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2026-03-21', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2026-03-22', 0);
-- Eid al-Adha 2026
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2026-05-26', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2026-05-27', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2026-05-28', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2026-05-29', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2026-05-30', 0);
-- Ramadan Feast 2027
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2027-03-09', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2027-03-10', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2027-03-11', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2027-03-12', 0);
-- Eid al-Adha 2027
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2027-05-16', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2027-05-17', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2027-05-18', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2027-05-19', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2027-05-20', 0);
-- Ramadan Feast 2028
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2028-02-26', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2028-02-27', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2028-02-28', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2028-02-29', 0);
-- Eid al-Adha 2028
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2028-05-04', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2028-05-05', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2028-05-06', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2028-05-07', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2028-05-08', 0);
-- Ramadan Feast 2029
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2029-02-14', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2029-02-15', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2029-02-16', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2029-02-17', 0);
-- Eid al-Adha 2029
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2029-04-23', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2029-04-24', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2029-04-25', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2029-04-26', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2029-04-27', 0);
-- Ramadan Feast 2030
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2030-02-04', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2030-02-05', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2030-02-06', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2030-02-07', 0);
-- Eid al-Adha 2030
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2030-04-12', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2030-04-13', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2030-04-14', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2030-04-15', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2030-04-16', 0);
-- Ramadan Feast 2031
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2031-01-24', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2031-01-25', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2031-01-26', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2031-01-27', 0);
-- Eid al-Adha 2031
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2031-04-01', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2031-04-02', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2031-04-03', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2031-04-04', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2031-04-05', 0);
-- Ramadan Feast 2032
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2032-01-13', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2032-01-14', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2032-01-15', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2032-01-16', 0);
-- Eid al-Adha 2032
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2032-03-21', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2032-03-22', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2032-03-23', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2032-03-24', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2032-03-25', 0);
-- Ramadan Feast 2033
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Ramadan Feast Eve', 4, NULL, NULL, '2033-01-02', 1),
         ('Ramadan Feast Day 1', 4, NULL, NULL, '2033-01-03', 0),
         ('Ramadan Feast Day 2', 4, NULL, NULL, '2033-01-04', 0),
         ('Ramadan Feast Day 3', 4, NULL, NULL, '2033-01-05', 0);
-- Eid al-Adha 2033
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Eid al-Adha Eve', 4, NULL, NULL, '2033-03-10', 1),
         ('Eid al-Adha Day 1', 4, NULL, NULL, '2033-03-11', 0),
         ('Eid al-Adha Day 2', 4, NULL, NULL, '2033-03-12', 0),
         ('Eid al-Adha Day 3', 4, NULL, NULL, '2033-03-13', 0),
         ('Eid al-Adha Day 4', 4, NULL, NULL, '2033-03-14', 0);

-- US Floating Holidays
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2024-01-15', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2024-02-19', 0),
         ('Good Friday', 1, NULL, NULL, '2024-03-29', 0),
         ('Memorial Day', 1, NULL, NULL, '2024-05-27', 0),
         ('Labor Day', 1, NULL, NULL, '2024-09-02', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2024-11-28', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2025-01-20', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2025-02-17', 0),
         ('Good Friday', 1, NULL, NULL, '2025-04-18', 0),
         ('Memorial Day', 1, NULL, NULL, '2025-05-26', 0),
         ('Labor Day', 1, NULL, NULL, '2025-09-01', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2025-11-27', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2026-01-19', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2026-02-16', 0),
         ('Good Friday', 1, NULL, NULL, '2026-04-03', 0),
         ('Memorial Day', 1, NULL, NULL, '2026-05-25', 0),
         ('Labor Day', 1, NULL, NULL, '2026-09-07', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2026-11-26', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2027-01-18', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2027-02-15', 0),
         ('Good Friday', 1, NULL, NULL, '2027-03-26', 0),
         ('Memorial Day', 1, NULL, NULL, '2027-05-31', 0),
         ('Labor Day', 1, NULL, NULL, '2027-09-06', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2027-11-25', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2028-01-17', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2028-02-21', 0),
         ('Good Friday', 1, NULL, NULL, '2028-04-14', 0),
         ('Memorial Day', 1, NULL, NULL, '2028-05-29', 0),
         ('Labor Day', 1, NULL, NULL, '2028-09-04', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2028-11-23', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2029-01-15', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2029-02-19', 0),
         ('Good Friday', 1, NULL, NULL, '2029-03-30', 0),
         ('Memorial Day', 1, NULL, NULL, '2029-05-28', 0),
         ('Labor Day', 1, NULL, NULL, '2029-09-03', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2029-11-22', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2030-01-21', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2030-02-18', 0),
         ('Good Friday', 1, NULL, NULL, '2030-04-19', 0),
         ('Memorial Day', 1, NULL, NULL, '2030-05-27', 0),
         ('Labor Day', 1, NULL, NULL, '2030-09-02', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2030-11-28', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2031-01-20', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2031-02-17', 0),
         ('Good Friday', 1, NULL, NULL, '2031-04-11', 0),
         ('Memorial Day', 1, NULL, NULL, '2031-05-26', 0),
         ('Labor Day', 1, NULL, NULL, '2031-09-01', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2031-11-27', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2032-01-19', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2032-02-16', 0),
         ('Good Friday', 1, NULL, NULL, '2032-03-26', 0),
         ('Memorial Day', 1, NULL, NULL, '2032-05-31', 0),
         ('Labor Day', 1, NULL, NULL, '2032-09-06', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2032-11-25', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Martin Luther King Jr. Day', 1, NULL, NULL, '2033-01-17', 0),
         ('Presidents'' Day', 1, NULL, NULL, '2033-02-21', 0),
         ('Good Friday', 1, NULL, NULL, '2033-04-15', 0),
         ('Memorial Day', 1, NULL, NULL, '2033-05-30', 0),
         ('Labor Day', 1, NULL, NULL, '2033-09-05', 0),
         ('Thanksgiving Day', 1, NULL, NULL, '2033-11-24', 0);

-- UK Floating Holidays
INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2024-03-29', 0),
         ('Easter Monday', 10, NULL, NULL, '2024-04-01', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2024-05-06', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2024-05-27', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2024-08-26', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2025-04-18', 0),
         ('Easter Monday', 10, NULL, NULL, '2025-04-21', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2025-05-05', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2025-05-26', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2025-08-25', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2026-04-03', 0),
         ('Easter Monday', 10, NULL, NULL, '2026-04-06', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2026-05-04', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2026-05-25', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2026-08-31', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2027-03-26', 0),
         ('Easter Monday', 10, NULL, NULL, '2027-03-29', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2027-05-03', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2027-05-31', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2027-08-30', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2028-04-14', 0),
         ('Easter Monday', 10, NULL, NULL, '2028-04-17', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2028-05-01', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2028-05-29', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2028-08-28', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2029-03-30', 0),
         ('Easter Monday', 10, NULL, NULL, '2029-04-02', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2029-05-07', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2029-05-28', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2029-08-27', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2030-04-19', 0),
         ('Easter Monday', 10, NULL, NULL, '2030-04-22', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2030-05-06', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2030-05-27', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2030-08-26', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2031-04-11', 0),
         ('Easter Monday', 10, NULL, NULL, '2031-04-14', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2031-05-05', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2031-05-26', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2031-08-25', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2032-03-26', 0),
         ('Easter Monday', 10, NULL, NULL, '2032-03-29', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2032-05-03', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2032-05-31', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2032-08-30', 0);

INSERT INTO Holidays (HolidayName, ExchangeId, Month, Day, HolidayDate, IsHalfDay) VALUES
         ('Good Friday', 10, NULL, NULL, '2033-04-15', 0),
         ('Easter Monday', 10, NULL, NULL, '2033-04-18', 0),
         ('Early May Bank Holiday', 10, NULL, NULL, '2033-05-02', 0),
         ('Spring Bank Holiday', 10, NULL, NULL, '2033-05-30', 0),
         ('Summer Bank Holiday', 10, NULL, NULL, '2033-08-29', 0);
