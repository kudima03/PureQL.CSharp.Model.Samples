using PureQL.CSharp.Model.Samples.Queries.TimeZones;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.TimeZones;

public sealed record LocalMonthIdentitiesQueryTests
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
                      "alias": "id",
                      "expression": {
                        "source": "schema_with_foreign_keys.users",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "back_again",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "datetimeDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "operator": "datetimeAddMonths",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "operator": "asDatetime",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "zone": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000385-0000-0000-0000-000000000000"
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/New_York"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            },
                            "months": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 7
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "right": {
                            "operator": "asDatetime",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 7
                        }
                      }
                    },
                    {
                      "alias": "swapped_is_negated",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "datetimeDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-08T12:00:00Z"
                          },
                          "right": {
                            "operator": "asDatetime",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "right": {
                          "operator": "subtract",
                          "type": {
                            "name": "integer"
                          },
                          "values": [
                            {
                              "type": {
                                "name": "integer"
                              },
                              "value": 0
                            },
                            {
                              "operator": "datetimeDiffMonths",
                              "type": {
                                "name": "integer"
                              },
                              "left": {
                                "operator": "asDatetime",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "signup_date",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "zone": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_tenant_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "uuid"
                                      },
                                      "value": "00000385-0000-0000-0000-000000000000"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "America/New_York"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Asia/Tokyo"
                                  }
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "datetime"
                                },
                                "value": "2024-06-08T12:00:00Z"
                              },
                              "zone": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000385-0000-0000-0000-000000000000"
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/New_York"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            }
                          ]
                        }
                      }
                    },
                    {
                      "alias": "from_parts",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "datetimeAddMonths",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "operator": "asDatetime",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "months": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "right": {
                          "operator": "datetimeFromParts",
                          "type": {
                            "name": "datetime"
                          },
                          "date": {
                            "operator": "dateAddMonths",
                            "type": {
                              "name": "date"
                            },
                            "operand": {
                              "operator": "asDate",
                              "type": {
                                "name": "date"
                              },
                              "operand": {
                                "operator": "asDatetime",
                                "type": {
                                  "name": "datetime"
                                },
                                "operand": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "signup_date",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "zone": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_tenant_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "uuid"
                                      },
                                      "value": "00000385-0000-0000-0000-000000000000"
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "America/New_York"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Asia/Tokyo"
                                  }
                                }
                              },
                              "zone": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000385-0000-0000-0000-000000000000"
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/New_York"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            },
                            "months": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 1
                            }
                          },
                          "time": {
                            "operator": "asTime",
                            "type": {
                              "name": "time"
                            },
                            "operand": {
                              "operator": "asDatetime",
                              "type": {
                                "name": "datetime"
                              },
                              "operand": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "signup_date",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "zone": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_tenant_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000385-0000-0000-0000-000000000000"
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "America/New_York"
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Asia/Tokyo"
                                }
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "months_between_local_days",
                      "expression": {
                        "operator": "dateDiffMonths",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-08T12:00:00Z"
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "right": {
                          "operator": "asDate",
                          "type": {
                            "name": "date"
                          },
                          "operand": {
                            "operator": "asDatetime",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "start_of_the_next_local_month",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "operator": "startOfMonth",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-08T12:00:00Z"
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000385-0000-0000-0000-000000000000"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/New_York"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Asia/Tokyo"
                          }
                        }
                      }
                    },
                    {
                      "alias": "start_of_the_previous_local_quarter",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "operator": "startOfQuarter",
                          "type": {
                            "name": "datetime"
                          },
                          "operand": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-08T12:00:00Z"
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -3
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000385-0000-0000-0000-000000000000"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/New_York"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Asia/Tokyo"
                          }
                        }
                      }
                    },
                    {
                      "alias": "same_local_time_a_year_ago",
                      "expression": {
                        "operator": "datetimeAddMonths",
                        "type": {
                          "name": "datetime"
                        },
                        "operand": {
                          "type": {
                            "name": "datetime"
                          },
                          "value": "2024-06-08T12:00:00Z"
                        },
                        "months": {
                          "type": {
                            "name": "integer"
                          },
                          "value": -12
                        },
                        "zone": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_tenant_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000385-0000-0000-0000-000000000000"
                            }
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "America/New_York"
                          },
                          "else": {
                            "type": {
                              "name": "string"
                            },
                            "value": "Asia/Tokyo"
                          }
                        }
                      }
                    },
                    {
                      "alias": "age_in_whole_years",
                      "expression": {
                        "operator": "integerDivide",
                        "type": {
                          "name": "integer"
                        },
                        "left": {
                          "operator": "datetimeDiffMonths",
                          "type": {
                            "name": "integer"
                          },
                          "left": {
                            "type": {
                              "name": "datetime"
                            },
                            "value": "2024-06-08T12:00:00Z"
                          },
                          "right": {
                            "operator": "asDatetime",
                            "type": {
                              "name": "datetime"
                            },
                            "operand": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "signup_date",
                              "type": {
                                "name": "date"
                              }
                            },
                            "zone": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_tenant_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000385-0000-0000-0000-000000000000"
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "America/New_York"
                              },
                              "else": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Asia/Tokyo"
                              }
                            }
                          },
                          "zone": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_tenant_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000385-0000-0000-0000-000000000000"
                              }
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "America/New_York"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Asia/Tokyo"
                            }
                          }
                        },
                        "right": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 12
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "column": "id"
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new LocalMonthIdentitiesQuery().Value).TextValue
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
                      "name": "id",
                      "type": "uuid"
                    },
                    {
                      "name": "back_again",
                      "type": "bool"
                    },
                    {
                      "name": "swapped_is_negated",
                      "type": "bool"
                    },
                    {
                      "name": "from_parts",
                      "type": "bool"
                    },
                    {
                      "name": "months_between_local_days",
                      "type": "long"
                    },
                    {
                      "name": "start_of_the_next_local_month",
                      "type": "datetime"
                    },
                    {
                      "name": "start_of_the_previous_local_quarter",
                      "type": "datetime"
                    },
                    {
                      "name": "same_local_time_a_year_ago",
                      "type": "datetime"
                    },
                    {
                      "name": "age_in_whole_years",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "52",
                      "2024-07-01T04:00:00Z",
                      "2024-01-01T05:00:00Z",
                      "2023-06-08T12:00:00Z",
                      "4"
                    ],
                    [
                      "00000002-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "38",
                      "2024-07-01T04:00:00Z",
                      "2024-01-01T05:00:00Z",
                      "2023-06-08T12:00:00Z",
                      "3"
                    ],
                    [
                      "00000003-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "58",
                      "2024-07-01T04:00:00Z",
                      "2024-01-01T05:00:00Z",
                      "2023-06-08T12:00:00Z",
                      "4"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "19",
                      "2024-06-30T15:00:00Z",
                      "2023-12-31T15:00:00Z",
                      "2023-06-08T12:00:00Z",
                      "1"
                    ],
                    [
                      "00000005-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "15",
                      "2024-06-30T15:00:00Z",
                      "2023-12-31T15:00:00Z",
                      "2023-06-08T12:00:00Z",
                      "1"
                    ],
                    [
                      "00000006-0000-0000-0000-000000000000",
                      "True",
                      "True",
                      "True",
                      "52",
                      "2024-06-30T15:00:00Z",
                      "2023-12-31T15:00:00Z",
                      "2023-06-08T12:00:00Z",
                      "4"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new LocalMonthIdentitiesQuery().Result).TextValue
        );
    }
}
