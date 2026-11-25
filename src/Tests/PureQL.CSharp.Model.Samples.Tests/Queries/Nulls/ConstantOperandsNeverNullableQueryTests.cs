using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record ConstantOperandsNeverNullableQueryTests
{
    [Fact]
    public void ValueSerializesToExpectedJson()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "from": {
                    "entity": "schema_with_foreign_keys.users"
                  },
                  "select": [
                    {
                      "alias": "name_nfc",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "e\u0301"
                            }
                          ]
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "middle_nfkd",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "nullIfEqual",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "left": {
                            "operator": "concat",
                            "type": {
                              "name": "string"
                            },
                            "values": [
                              {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
                                }
                              },
                              {
                                "type": {
                                  "name": "string"
                                },
                                "value": "\u00e9"
                              }
                            ]
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Bob\u00e9"
                          }
                        },
                        "form": "NFKD"
                      }
                    },
                    {
                      "alias": "whole_age",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "integer"
                        },
                        "operand": {
                          "operator": "divide",
                          "type": {
                            "name": "decimal"
                          },
                          "values": [
                            {
                              "operator": "dateDiffDays",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-30"
                              },
                              "right": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "365.25"
                            }
                          ]
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "rate",
                      "expression": {
                        "operator": "round",
                        "type": {
                          "name": "decimal"
                        },
                        "operand": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "2.345"
                        },
                        "digits": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "mode": "halfEven"
                      }
                    },
                    {
                      "alias": "has_domain",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "@"
                            },
                            {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "example.org"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "mail.net"
                              }
                            }
                          ]
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^[A-C][a-z]*@example[.]org$"
                        }
                      }
                    },
                    {
                      "alias": "domain",
                      "expression": {
                        "operator": "extractRegex",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "concat",
                          "type": {
                            "name": "string"
                          },
                          "values": [
                            {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            },
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": "@"
                            },
                            {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_active",
                                "type": {
                                  "name": "boolean"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "example.org"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "mail.net"
                              }
                            }
                          ]
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "@(.+)$"
                        },
                        "group": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "all_names",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
                        "orderBy": [
                          {
                            "expression": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_name",
                              "type": {
                                "name": "string"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "signup_quantile",
                      "expression": {
                        "operator": "percentileDiscrete",
                        "type": {
                          "name": "date"
                        },
                        "selector": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "fraction": {
                          "type": {
                            "name": "decimal"
                          },
                          "value": "0.75"
                        }
                      }
                    },
                    {
                      "alias": "bucket",
                      "expression": {
                        "operator": "ntile",
                        "type": {
                          "name": "integer"
                        },
                        "buckets": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 4
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "earlier_signup",
                      "expression": {
                        "operator": "previous",
                        "type": {
                          "name": "date"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "offset": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 2
                        },
                        "default": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "signup_date",
                          "type": {
                            "name": "date"
                          }
                        },
                        "over": {
                          "orderBy": [
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              }
                            },
                            {
                              "expression": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_name",
                          "type": {
                            "name": "string"
                          }
                        },
                        "form": "NFKC"
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new ConstantOperandsNeverNullableQuery().Value).TextValue
        );
    }

    [Fact]
    public void ResultMatchesExpectedRows()
    {
        Assert.Equal(
            new ExpectedJson(
                /*lang=json,strict*/
                """
                {
                  "name": "",
                  "columns": [
                    {
                      "name": "name_nfc",
                      "type": "string"
                    },
                    {
                      "name": "middle_nfkd",
                      "type": "string"
                    },
                    {
                      "name": "whole_age",
                      "type": "long"
                    },
                    {
                      "name": "rate",
                      "type": "double"
                    },
                    {
                      "name": "has_domain",
                      "type": "bool"
                    },
                    {
                      "name": "domain",
                      "type": "string"
                    },
                    {
                      "name": "all_names",
                      "type": "string"
                    },
                    {
                      "name": "signup_quantile",
                      "type": "date"
                    },
                    {
                      "name": "bucket",
                      "type": "long"
                    },
                    {
                      "name": "earlier_signup",
                      "type": "date"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Ann\u00e9",
                      "Anne\u0301",
                      "4",
                      "2.34",
                      "True",
                      "example.org",
                      "Ann, Bob, Cara, Dan, Eve, Fay",
                      "2022-11-05",
                      "1",
                      "2020-01-15"
                    ],
                    [
                      "Bob\u00e9",
                      "",
                      "3",
                      "2.34",
                      "False",
                      "mail.net",
                      "Ann, Bob, Cara, Dan, Eve, Fay",
                      "2022-11-05",
                      "2",
                      "2020-01-15"
                    ],
                    [
                      "Cara\u00e9",
                      "Carae\u0301",
                      "5",
                      "2.34",
                      "True",
                      "example.org",
                      "Ann, Bob, Cara, Dan, Eve, Fay",
                      "2022-11-05",
                      "1",
                      "2019-07-10"
                    ],
                    [
                      "Dan\u00e9",
                      "Dane\u0301",
                      "2",
                      "2.34",
                      "False",
                      "example.org",
                      "Ann, Bob, Cara, Dan, Eve, Fay",
                      "2022-11-05",
                      "3",
                      "2020-01-15"
                    ],
                    [
                      "Eve\u00e9",
                      "Evee\u0301",
                      "1",
                      "2.34",
                      "False",
                      "mail.net",
                      "Ann, Bob, Cara, Dan, Eve, Fay",
                      "2022-11-05",
                      "4",
                      "2021-03-20"
                    ],
                    [
                      "Fay\u00e9",
                      "Faye\u0301",
                      "4",
                      "2.34",
                      "False",
                      "example.org",
                      "Ann, Bob, Cara, Dan, Eve, Fay",
                      "2022-11-05",
                      "2",
                      "2019-07-10"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new ConstantOperandsNeverNullableQuery().Result).TextValue
        );
    }
}
