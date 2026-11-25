using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record NormalizeMatchingOverJoinsQueryTests
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
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
                      "alias": "other",
                      "on": {
                        "operator": "and",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "normalize",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "foldCase",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "\uff29\uff2e\uff26\uff2f@EXAMPLE.COM"
                                  },
                                  "else": {
                                    "operator": "if",
                                    "type": {
                                      "name": "string"
                                    },
                                    "then": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "info@example.com"
                                    },
                                    "else": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "sales@example.com"
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
                                    }
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "schema_with_foreign_keys.users",
                                      "field": "user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "uuid"
                                      },
                                      "value": "00000002-0000-0000-0000-000000000000"
                                    }
                                  }
                                }
                              },
                              "form": "NFKC"
                            },
                            "right": {
                              "operator": "normalize",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "operator": "foldCase",
                                "type": {
                                  "name": "string"
                                },
                                "operand": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "\uff29\uff2e\uff26\uff2f@EXAMPLE.COM"
                                  },
                                  "else": {
                                    "operator": "if",
                                    "type": {
                                      "name": "string"
                                    },
                                    "then": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "info@example.com"
                                    },
                                    "else": {
                                      "type": {
                                        "name": "string"
                                      },
                                      "value": "sales@example.com"
                                    },
                                    "condition": {
                                      "operator": "equal",
                                      "type": {
                                        "name": "boolean"
                                      },
                                      "left": {
                                        "source": "other",
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
                                    }
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "other",
                                      "field": "user_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "uuid"
                                      },
                                      "value": "00000002-0000-0000-0000-000000000000"
                                    }
                                  }
                                }
                              },
                              "form": "NFKC"
                            }
                          },
                          {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "source": "other",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "kind": "left",
                      "entity": "schema_with_foreign_keys.orders",
                      "on": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_user_id",
                          "type": {
                            "name": "uuid"
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.users",
                          "field": "user_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      }
                    }
                  ],
                  "where": {
                    "operator": "in",
                    "type": {
                      "name": "boolean"
                    },
                    "operand": {
                      "operator": "normalize",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "operator": "if",
                        "type": {
                          "name": "string"
                        },
                        "then": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Chile"
                        },
                        "else": {
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "then": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Bresil"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Br\u00e9sil"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000002-0000-0000-0000-000000000000"
                              }
                            }
                          },
                          "else": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "P\u00c9RU"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "Peru"
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000005-0000-0000-0000-000000000000"
                              }
                            }
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
                          }
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.users",
                            "field": "user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "uuid"
                            },
                            "value": "00000006-0000-0000-0000-000000000000"
                          }
                        }
                      },
                      "form": "NFKC"
                    },
                    "list": {
                      "type": {
                        "name": "stringList"
                      },
                      "value": [
                        "Br\u00e9sil",
                        "Peru"
                      ]
                    }
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
                      "alias": "other_id",
                      "expression": {
                        "source": "other",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "same_last_name",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "LEF\u00c8VRE"
                            },
                            "else": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Lef\u00e8vre"
                              },
                              "else": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "MULLER"
                                },
                                "else": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Lefe\u0300vre"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Mu\u0308ller"
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
                                  }
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.users",
                                    "field": "user_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000006-0000-0000-0000-000000000000"
                                  }
                                }
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000003-0000-0000-0000-000000000000"
                                }
                              }
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000002-0000-0000-0000-000000000000"
                              }
                            }
                          },
                          "form": "NFC"
                        },
                        "right": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "LEF\u00c8VRE"
                            },
                            "else": {
                              "operator": "if",
                              "type": {
                                "name": "string"
                              },
                              "then": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "Lef\u00e8vre"
                              },
                              "else": {
                                "operator": "if",
                                "type": {
                                  "name": "string"
                                },
                                "then": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "MULLER"
                                },
                                "else": {
                                  "operator": "if",
                                  "type": {
                                    "name": "string"
                                  },
                                  "then": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Lefe\u0300vre"
                                  },
                                  "else": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "Mu\u0308ller"
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "source": "other",
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
                                  }
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "other",
                                    "field": "user_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "uuid"
                                    },
                                    "value": "00000006-0000-0000-0000-000000000000"
                                  }
                                }
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "other",
                                  "field": "user_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "value": "00000003-0000-0000-0000-000000000000"
                                }
                              }
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "other",
                                "field": "user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "uuid"
                                },
                                "value": "00000002-0000-0000-0000-000000000000"
                              }
                            }
                          },
                          "form": "NFC"
                        }
                      }
                    },
                    {
                      "alias": "status",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_status",
                          "type": {
                            "name": "string",
                            "nullable": true
                          }
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "status_is_paid",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "normalize",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "operand": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string",
                              "nullable": true
                            }
                          },
                          "form": "NFKC"
                        },
                        "right": {
                          "type": {
                            "name": "string"
                          },
                          "value": "paid"
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
                          "operator": "if",
                          "type": {
                            "name": "string"
                          },
                          "then": {
                            "type": {
                              "name": "string"
                            },
                            "value": "\uff29\uff2e\uff26\uff2f@EXAMPLE.COM"
                          },
                          "else": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "info@example.com"
                            },
                            "else": {
                              "type": {
                                "name": "string"
                              },
                              "value": "sales@example.com"
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
                            }
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.users",
                              "field": "user_id",
                              "type": {
                                "name": "uuid"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "uuid"
                              },
                              "value": "00000002-0000-0000-0000-000000000000"
                            }
                          }
                        },
                        "form": "NFC"
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
                    },
                    {
                      "expression": {
                        "source": "other",
                        "field": "user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_status",
                        "type": {
                          "name": "string",
                          "nullable": true
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NormalizeMatchingOverJoinsQuery().Value).TextValue
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
                      "name": "other_id",
                      "type": "uuid"
                    },
                    {
                      "name": "same_last_name",
                      "type": "bool"
                    },
                    {
                      "name": "status",
                      "type": "string"
                    },
                    {
                      "name": "status_is_paid",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "False",
                      "pending",
                      "False"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000002-0000-0000-0000-000000000000",
                      "False",
                      "shipped",
                      "False"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "True",
                      "pending",
                      "False"
                    ],
                    [
                      "00000001-0000-0000-0000-000000000000",
                      "00000003-0000-0000-0000-000000000000",
                      "True",
                      "shipped",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "00000005-0000-0000-0000-000000000000",
                      "True",
                      "pending",
                      "False"
                    ],
                    [
                      "00000004-0000-0000-0000-000000000000",
                      "00000006-0000-0000-0000-000000000000",
                      "False",
                      "pending",
                      "False"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NormalizeMatchingOverJoinsQuery().Result).TextValue
        );
    }
}
