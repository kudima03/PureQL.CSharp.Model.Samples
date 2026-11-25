using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record RemoveDiacriticsInATotalQueryTests
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
                  "where": {
                    "operator": "notEqual",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "removeDiacritics",
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
                        }
                      }
                    },
                    "right": {
                      "operator": "removeDiacritics",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "BR\u00c9SIL"
                        }
                      }
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "with_accents",
                      "expression": {
                        "operator": "count",
                        "type": {
                          "name": "integer"
                        },
                        "predicate": {
                          "operator": "notEqual",
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
                            "operator": "removeDiacritics",
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
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_last_name",
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "operator": "min",
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "selector": {
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_plain_last_name",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "removeDiacritics",
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "countries",
                      "expression": {
                        "operator": "concatAll",
                        "type": {
                          "name": "string"
                        },
                        "selector": {
                          "operator": "removeDiacritics",
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
                              "operator": "removeDiacritics",
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
                              }
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "any_like_needle",
                      "expression": {
                        "operator": "any",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "contains",
                          "type": {
                            "name": "boolean"
                          },
                          "operand": {
                            "operator": "removeDiacritics",
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
                              }
                            }
                          },
                          "pattern": {
                            "operator": "removeDiacritics",
                            "type": {
                              "name": "string"
                            },
                            "operand": {
                              "operator": "foldCase",
                              "type": {
                                "name": "string"
                              },
                              "operand": {
                                "type": {
                                  "name": "string"
                                },
                                "value": "\u00dcLL"
                              }
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "most_common_key",
                      "expression": {
                        "operator": "mode",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
                          "operator": "removeDiacritics",
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
                            }
                          }
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new RemoveDiacriticsInATotalQuery().Value).TextValue
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
                      "name": "with_accents",
                      "type": "long"
                    },
                    {
                      "name": "first_last_name",
                      "type": "string"
                    },
                    {
                      "name": "first_plain_last_name",
                      "type": "string"
                    },
                    {
                      "name": "countries",
                      "type": "string"
                    },
                    {
                      "name": "any_like_needle",
                      "type": "bool"
                    },
                    {
                      "name": "most_common_key",
                      "type": "string"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "2",
                      "MULLER",
                      "MULLER",
                      "Chile, PERU, Peru",
                      "True",
                      "muller"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RemoveDiacriticsInATotalQuery().Result).TextValue
        );
    }
}
