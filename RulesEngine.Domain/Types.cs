using System;
using System.Collections.Generic;
using System.Linq;

namespace Hein.RulesEngine.Domain
{
    public class RuleType
    {
        private static IDictionary<string, Type> _dictionary = new Dictionary<string, Type>()
        {
            { "String",     typeof(string)      },
            { "DateTime",   typeof(DateTime)    },
            { "Number",     typeof(long)        },
            { "Bool",       typeof(bool)        },
            { "Decimal",    typeof(double)      },
            { "Boolean",    typeof(bool)        },
            { "string",     typeof(string)      },
            { "dateTime",   typeof(DateTime)    },
            { "number",     typeof(long)        },
            { "bool",       typeof(bool)        },
            { "decimal",    typeof(double)      },
            { "boolean",    typeof(bool)        },
            { "int",        typeof(long)        },
            { "Int",        typeof(long)        },
            { "Integer",    typeof(long)        },
            { "integer",    typeof(long)        },
            { "datetime",   typeof(DateTime)    },
            { "long",       typeof(long)        },
            { "Long",       typeof(long)        }
        };

        public static Type GetType(string typeName)
        {
            return _dictionary.FirstOrDefault(x => x.Key == typeName).Value;
        }
    }
}
