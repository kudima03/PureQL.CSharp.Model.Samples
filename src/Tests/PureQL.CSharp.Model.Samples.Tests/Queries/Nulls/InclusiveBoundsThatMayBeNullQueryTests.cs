using PureQL.CSharp.Model.Samples.Queries.Nulls;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Nulls;

public sealed record InclusiveBoundsThatMayBeNullQueryTests
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
                    "entity": "schema_with_foreign_keys.orders"
                  },
                  "where": {
                    "operator": "and",
                    "type": {
                      "name": "boolean"
                    },
                    "conditions": [
                      {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          }
                        },
                        "right": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "then": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-02"
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          }
                        }
                      },
                      {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          }
                        },
                        "right": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "then": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-05"
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          }
                        }
                      }
                    ]
                  },
                  "select": [
                    {
                      "alias": "order_id",
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    {
                      "alias": "shipped_date",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "then": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "else": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        }
                      }
                    },
                    {
                      "alias": "at_least_from",
                      "expression": {
                        "operator": "greaterThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "placed_on",
                            "type": {
                              "name": "date"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          }
                        },
                        "right": {
                          "operator": "if",
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "then": {
                            "type": {
                              "name": "date"
                            },
                            "value": "2024-06-02"
                          },
                          "else": {
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "after_from_or_equal_to_it",
                      "expression": {
                        "operator": "or",
                        "type": {
                          "name": "boolean"
                        },
                        "conditions": [
                          {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              }
                            },
                            "right": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-02"
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "100"
                                }
                              }
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              }
                            },
                            "right": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-02"
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "100"
                                }
                              }
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "both_ways_is_equal",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "and",
                          "type": {
                            "name": "boolean"
                          },
                          "conditions": [
                            {
                              "operator": "lessThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "then": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "else": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_status",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "shipped"
                                  }
                                }
                              },
                              "right": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "then": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-02"
                                },
                                "else": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "condition": {
                                  "operator": "greaterThan",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "100"
                                  }
                                }
                              }
                            },
                            {
                              "operator": "greaterThanOrEqual",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "then": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "placed_on",
                                  "type": {
                                    "name": "date"
                                  }
                                },
                                "else": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "condition": {
                                  "operator": "equal",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_status",
                                    "type": {
                                      "name": "string"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "string"
                                    },
                                    "value": "shipped"
                                  }
                                }
                              },
                              "right": {
                                "operator": "if",
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "then": {
                                  "type": {
                                    "name": "date"
                                  },
                                  "value": "2024-06-02"
                                },
                                "else": {
                                  "type": {
                                    "name": "date",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "condition": {
                                  "operator": "greaterThan",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_total",
                                    "type": {
                                      "name": "decimal"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "decimal"
                                    },
                                    "value": "100"
                                  }
                                }
                              }
                            }
                          ]
                        },
                        "right": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "if",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "then": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "shipped"
                              }
                            }
                          },
                          "right": {
                            "operator": "if",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "then": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-02"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "not_shipped_and_no_bound",
                      "expression": {
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
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          },
                          {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-02"
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "100"
                                }
                              }
                            },
                            "right": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "not_before_is_not_at_least",
                      "expression": {
                        "operator": "notEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "not",
                          "type": {
                            "name": "boolean"
                          },
                          "condition": {
                            "operator": "lessThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "placed_on",
                                "type": {
                                  "name": "date"
                                }
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_status",
                                  "type": {
                                    "name": "string"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "shipped"
                                }
                              }
                            },
                            "right": {
                              "operator": "if",
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "then": {
                                "type": {
                                  "name": "date"
                                },
                                "value": "2024-06-02"
                              },
                              "else": {
                                "type": {
                                  "name": "date",
                                  "nullable": true
                                },
                                "value": null
                              },
                              "condition": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_total",
                                  "type": {
                                    "name": "decimal"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "decimal"
                                  },
                                  "value": "100"
                                }
                              }
                            }
                          }
                        },
                        "right": {
                          "operator": "greaterThanOrEqual",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "if",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "then": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "placed_on",
                              "type": {
                                "name": "date"
                              }
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "equal",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_status",
                                "type": {
                                  "name": "string"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "shipped"
                              }
                            }
                          },
                          "right": {
                            "operator": "if",
                            "type": {
                              "name": "date",
                              "nullable": true
                            },
                            "then": {
                              "type": {
                                "name": "date"
                              },
                              "value": "2024-06-02"
                            },
                            "else": {
                              "type": {
                                "name": "date",
                                "nullable": true
                              },
                              "value": null
                            },
                            "condition": {
                              "operator": "greaterThan",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_total",
                                "type": {
                                  "name": "decimal"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "decimal"
                                },
                                "value": "100"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "coupon_at_most_tenant",
                      "expression": {
                        "operator": "lessThanOrEqual",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_status",
                              "type": {
                                "name": "string"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "string"
                              },
                              "value": "shipped"
                            }
                          }
                        },
                        "right": {
                          "operator": "if",
                          "type": {
                            "name": "uuid",
                            "nullable": true
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_tenant_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "else": {
                            "type": {
                              "name": "uuid",
                              "nullable": true
                            },
                            "value": null
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_total",
                              "type": {
                                "name": "decimal"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "decimal"
                              },
                              "value": "100"
                            }
                          }
                        }
                      }
                    }
                  ],
                  "orderBy": [
                    {
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "date",
                          "nullable": true
                        },
                        "then": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "placed_on",
                          "type": {
                            "name": "date"
                          }
                        },
                        "else": {
                          "type": {
                            "name": "date",
                            "nullable": true
                          },
                          "value": null
                        },
                        "condition": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_status",
                            "type": {
                              "name": "string"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "string"
                            },
                            "value": "shipped"
                          }
                        }
                      }
                    },
                    {
                      "expression": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new InclusiveBoundsThatMayBeNullQuery().Value).TextValue
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
                      "name": "order_id",
                      "type": "uuid"
                    },
                    {
                      "name": "shipped_date",
                      "type": "date"
                    },
                    {
                      "name": "at_least_from",
                      "type": "bool"
                    },
                    {
                      "name": "after_from_or_equal_to_it",
                      "type": "bool"
                    },
                    {
                      "name": "both_ways_is_equal",
                      "type": "bool"
                    },
                    {
                      "name": "not_shipped_and_no_bound",
                      "type": "bool"
                    },
                    {
                      "name": "not_before_is_not_at_least",
                      "type": "bool"
                    },
                    {
                      "name": "coupon_at_most_tenant",
                      "type": "bool"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000066-0000-0000-0000-000000000000",
                      "",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000",
                      "",
                      "True",
                      "True",
                      "True",
                      "True",
                      "False",
                      "True"
                    ],
                    [
                      "00000067-0000-0000-0000-000000000000",
                      "2024-06-03",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "True"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000",
                      "2024-06-05",
                      "True",
                      "True",
                      "True",
                      "False",
                      "False",
                      "True"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new InclusiveBoundsThatMayBeNullQuery().Result).TextValue
        );
    }
}
