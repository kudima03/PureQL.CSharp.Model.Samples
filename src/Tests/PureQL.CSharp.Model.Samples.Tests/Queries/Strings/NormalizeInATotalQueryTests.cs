using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record NormalizeInATotalQueryTests
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
                      "form": "NFC"
                    },
                    "right": {
                      "operator": "normalize",
                      "type": {
                        "name": "string"
                      },
                      "operand": {
                        "type": {
                          "name": "string"
                        },
                        "value": "Bre\u0301sil"
                      },
                      "form": "NFC"
                    }
                  },
                  "aggregate": [
                    {
                      "alias": "stored_decomposed",
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
                          }
                        }
                      }
                    },
                    {
                      "alias": "first_last_name",
                      "expression": {
                        "operator": "normalize",
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
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "first_normalized_last_name",
                      "expression": {
                        "operator": "min",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "selector": {
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
                        "separator": {
                          "type": {
                            "name": "string"
                          },
                          "value": ", "
                        },
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
                            }
                          }
                        ],
                        "distinct": true
                      }
                    },
                    {
                      "alias": "all_composed",
                      "expression": {
                        "operator": "all",
                        "type": {
                          "name": "boolean"
                        },
                        "predicate": {
                          "operator": "equal",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "if",
                            "type": {
                              "name": "string"
                            },
                            "then": {
                              "type": {
                                "name": "string"
                              },
                              "value": "\u00c1NN"
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
                                "value": "E\u0301ve"
                              },
                              "else": {
                                "source": "schema_with_foreign_keys.users",
                                "field": "user_name",
                                "type": {
                                  "name": "string"
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
                                "value": "\u00c1NN"
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
                                  "value": "E\u0301ve"
                                },
                                "else": {
                                  "source": "schema_with_foreign_keys.users",
                                  "field": "user_name",
                                  "type": {
                                    "name": "string"
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
                            "form": "NFC"
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
                          },
                          "form": "NFKC"
                        }
                      }
                    }
                  ]
                }
                """
            ).TextValue,
            new QueryJson(new NormalizeInATotalQuery().Value).TextValue
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
                      "name": "stored_decomposed",
                      "type": "long"
                    },
                    {
                      "name": "first_last_name",
                      "type": "string"
                    },
                    {
                      "name": "first_normalized_last_name",
                      "type": "string"
                    },
                    {
                      "name": "countries",
                      "type": "string"
                    },
                    {
                      "name": "all_composed",
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
                      "LEF\u00c8VRE",
                      "LEF\u00c8VRE",
                      "Bresil, Chile, Peru, P\u00c9RU",
                      "False",
                      "m\u00fcller"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NormalizeInATotalQuery().Result).TextValue
        );
    }
}
