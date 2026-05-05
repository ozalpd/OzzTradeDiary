INSERT INTO Exchanges
(Id, ExchangeCode, ExchangeName, CountryCode, DefaultCurrencyId, Timezone, IsAlwaysOpen, HasAnySymbol, DisplayOrder, IsActive) VALUES
-- United States
(1,  'NYSE',     'New York Stock Exchange',        'us', 1, 'America/New_York', 0, 0, 1000, 0),
(2,  'NASDAQ',   'NASDAQ Stock Market',            'us', 1, 'America/New_York', 0, 1, 1000, 0),
(3,  'CME',      'Chicago Mercantile Exchange',    'us', 1, 'America/Chicago',  0, 0, 1000, 1),
(8,  'AMEX',     'American Stock Exchange',        'us', 1, 'America/New_York', 0, 0, 1000, 0),
(9,  'CBOE',     'Chicago Board Options Exchange', 'us', 1, 'America/Chicago',  0, 0, 1000, 0),
-- United Kingdom
(10, 'LSE',      'London Stock Exchange',          'gb', 10, 'Europe/London',   0, 0, 1000, 0),
-- Türkiye
(4,  'BIST',     'Borsa Istanbul',                 'tr', 21, 'Europe/Istanbul', 0, 1, 1000, 1),
-- Japan
(11, 'TSE',      'Tokyo Stock Exchange',           'jp', 13, 'Asia/Tokyo',      0, 0, 1000, 0),
-- Hong Kong
(12, 'HKEX',     'Hong Kong Stock Exchange',       'hk', 14, 'Asia/Hong_Kong',  0, 0, 1000, 0),
-- Singapore
(14, 'SGX',      'Singapore Exchange',             'sg', 14, 'Asia/Singapore',  0, 0, 1000, 0),
-- China
(15, 'SSE',      'Shanghai Stock Exchange',        'cn', 14, 'Asia/Shanghai',   0, 0, 1000, 0),
(16, 'SZSE',     'Shenzhen Stock Exchange',        'cn', 14, 'Asia/Shanghai',   0, 0, 1000, 0),
-- South Korea
(17, 'KRX',      'Korea Exchange',                 'kr', 14, 'Asia/Seoul',      0, 0, 1000, 0),
-- Crypto (Always Open)
(5,  'BINGX',    'BingX',                          'sg', 2, 'UTC',              1, 1, 1000, 1),
(6,  'BYBIT',    'Bybit',                          'ae', 2, 'UTC',              1, 1, 1000, 1),
(7,  'COINBASE', 'Coinbase',                       'us', 1, 'UTC',              1, 0, 1000, 0),
(13, 'BINANCE',  'Binance',                        'ky', 2, 'UTC',              1, 0, 1000, 0);
