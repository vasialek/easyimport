using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models
{
    public enum DbTypeIso
    {
        // Not set
        __NOT_SET__,

        // Exact Numerics:
        INTEGER,
        SMALLINT,
        BIGINT,
        NUMERIC,
        DECIMAL,

        // Approximate Numerics:
        REAL,
        DOUBLE_PRECISION,
        FLOAT,

        // Binary Strings:
        BINARY,
        BINARY_VARYING,
        BINARY_LARGE_OBJECT,

        // Boolean:
        BOOLEAN,

        // Character Strings:
        CHARACTER,
        VARCHAR,
        CHARACTER_LARGE_OBJECT,
        NATIONAL_CHARACTER,
        NATIONAL_CHARACTER_VARYING,
        NATIONAL_CHARACTER_LARGE_OBJECT,

        // Datetimes:
        DATE,
        TIME_WITHOUT_TIMEZONE,
        TIMESTAMP_WITHOUT_TIMEZONE,
        TIME_WITH_TIMEZONE,
        TIMESTAMP_WITH_TIMEZONE,

        // Intervals:
        INTERVAL_DAY,
        INTERVAL_YEAR,

        // Collection Types:
        ARRAY,
        MULTISET,

        // Other Types:
        ROW,
        XML
    }
}
