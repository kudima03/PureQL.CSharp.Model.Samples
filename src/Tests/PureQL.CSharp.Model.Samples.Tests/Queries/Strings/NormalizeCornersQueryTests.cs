using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record NormalizeCornersQueryTests
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
                      "alias": "in_nfc",
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
                    },
                    {
                      "alias": "in_nfd",
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
                        "form": "NFD"
                      }
                    },
                    {
                      "alias": "in_nfkc",
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
                        "form": "NFKC"
                      }
                    },
                    {
                      "alias": "in_nfkd",
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
                        "form": "NFKD"
                      }
                    },
                    {
                      "alias": "normalized_twice",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
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
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "composed_after_decomposing",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
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
                          "form": "NFD"
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "of_a_literal",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "\ufb01anc\u00e9"
                        },
                        "form": "NFKC"
                      }
                    },
                    {
                      "alias": "of_a_nullable_literal",
                      "expression": {
                        "operator": "normalize",
                        "type": {
                          "name": "string",
                          "nullable": true
                        },
                        "operand": {
                          "type": {
                            "name": "string",
                            "nullable": true
                          },
                          "value": null
                        },
                        "form": "NFD"
                      }
                    },
                    {
                      "alias": "of_a_concat",
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
                            {
                              "type": {
                                "name": "string"
                              },
                              "value": " "
                            },
                            {
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
                          ]
                        },
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "fold_then_normalize",
                      "expression": {
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
                        "form": "NFC"
                      }
                    },
                    {
                      "alias": "canonical_caseless_key",
                      "expression": {
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
                            "form": "NFD"
                          }
                        },
                        "form": "NFD"
                      }
                    },
                    {
                      "alias": "accents_as_code_points",
                      "expression": {
                        "operator": "subtract",
                        "type": {
                          "name": "integer"
                        },
                        "values": [
                          {
                            "operator": "length",
                            "type": {
                              "name": "integer"
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
                              "form": "NFD"
                            }
                          },
                          {
                            "operator": "length",
                            "type": {
                              "name": "integer"
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
                        ]
                      }
                    },
                    {
                      "alias": "contains_needle",
                      "expression": {
                        "operator": "contains",
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
                        "pattern": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
                            "type": {
                              "name": "string"
                            },
                            "value": "e\u0300vr"
                          },
                          "form": "NFC"
                        }
                      }
                    },
                    {
                      "alias": "first_letter",
                      "expression": {
                        "operator": "substring",
                        "type": {
                          "name": "string"
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
                        },
                        "start": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        },
                        "length": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 1
                        }
                      }
                    },
                    {
                      "alias": "has_combining_marks",
                      "expression": {
                        "operator": "matchesRegex",
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
                          "form": "NFD"
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "[^a-zA-Z '-]"
                        }
                      }
                    },
                    {
                      "alias": "name_or_nickname",
                      "expression": {
                        "operator": "coalesce",
                        "type": {
                          "name": "string"
                        },
                        "values": [
                          {
                            "operator": "normalize",
                            "type": {
                              "name": "string",
                              "nullable": true
                            },
                            "operand": {
                              "operator": "if",
                              "type": {
                                "name": "string",
                                "nullable": true
                              },
                              "then": {
                                "operator": "if",
                                "type": {
                                  "name": "string",
                                  "nullable": true
                                },
                                "then": {
                                  "type": {
                                    "name": "string",
                                    "nullable": true
                                  },
                                  "value": null
                                },
                                "else": {
                                  "type": {
                                    "name": "string"
                                  },
                                  "value": "Zo\u00eb"
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
                                "type": {
                                  "name": "string",
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
                            "form": "NFC"
                          },
                          {
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
                        ]
                      }
                    },
                    {
                      "alias": "position_among_namesakes",
                      "expression": {
                        "operator": "rowNumber",
                        "type": {
                          "name": "integer"
                        },
                        "over": {
                          "partitionBy": [
                            {
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
            new QueryJson(new NormalizeCornersQuery().Value).TextValue
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
                      "name": "in_nfc",
                      "type": "string"
                    },
                    {
                      "name": "in_nfd",
                      "type": "string"
                    },
                    {
                      "name": "in_nfkc",
                      "type": "string"
                    },
                    {
                      "name": "in_nfkd",
                      "type": "string"
                    },
                    {
                      "name": "normalized_twice",
                      "type": "string"
                    },
                    {
                      "name": "composed_after_decomposing",
                      "type": "string"
                    },
                    {
                      "name": "of_a_literal",
                      "type": "string"
                    },
                    {
                      "name": "of_a_nullable_literal",
                      "type": "string"
                    },
                    {
                      "name": "of_a_concat",
                      "type": "string"
                    },
                    {
                      "name": "fold_then_normalize",
                      "type": "string"
                    },
                    {
                      "name": "canonical_caseless_key",
                      "type": "string"
                    },
                    {
                      "name": "accents_as_code_points",
                      "type": "long"
                    },
                    {
                      "name": "contains_needle",
                      "type": "bool"
                    },
                    {
                      "name": "first_letter",
                      "type": "string"
                    },
                    {
                      "name": "has_combining_marks",
                      "type": "bool"
                    },
                    {
                      "name": "name_or_nickname",
                      "type": "string"
                    },
                    {
                      "name": "position_among_namesakes",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "Lef\u00e8vre",
                      "Lefe\u0300vre",
                      "Lef\u00e8vre",
                      "Lefe\u0300vre",
                      "Lef\u00e8vre",
                      "Lef\u00e8vre",
                      "fianc\u00e9",
                      "",
                      "Ann Lef\u00e8vre",
                      "lef\u00e8vre",
                      "lefe\u0300vre",
                      "1",
                      "True",
                      "A",
                      "True",
                      "Zo\u00eb",
                      "1"
                    ],
                    [
                      "LEF\u00c8VRE",
                      "LEFE\u0300VRE",
                      "LEF\u00c8VRE",
                      "LEFE\u0300VRE",
                      "LEF\u00c8VRE",
                      "LEF\u00c8VRE",
                      "fianc\u00e9",
                      "",
                      "Bob LEF\u00c8VRE",
                      "lef\u00e8vre",
                      "lefe\u0300vre",
                      "1",
                      "False",
                      "B",
                      "True",
                      "Bob",
                      "2"
                    ],
                    [
                      "Lef\u00e8vre",
                      "Lefe\u0300vre",
                      "Lef\u00e8vre",
                      "Lefe\u0300vre",
                      "Lef\u00e8vre",
                      "Lef\u00e8vre",
                      "fianc\u00e9",
                      "",
                      "\u00c1NN Lef\u00e8vre",
                      "lef\u00e8vre",
                      "lefe\u0300vre",
                      "1",
                      "True",
                      "\u00c1",
                      "True",
                      "Zo\u00eb",
                      "3"
                    ],
                    [
                      "MULLER",
                      "MULLER",
                      "MULLER",
                      "MULLER",
                      "MULLER",
                      "MULLER",
                      "fianc\u00e9",
                      "",
                      "Fay MULLER",
                      "muller",
                      "muller",
                      "0",
                      "False",
                      "F",
                      "False",
                      "Fay",
                      "1"
                    ],
                    [
                      "M\u00fcller",
                      "Mu\u0308ller",
                      "M\u00fcller",
                      "Mu\u0308ller",
                      "M\u00fcller",
                      "M\u00fcller",
                      "fianc\u00e9",
                      "",
                      "Dan M\u00fcller",
                      "m\u00fcller",
                      "mu\u0308ller",
                      "1",
                      "False",
                      "D",
                      "True",
                      "Dan",
                      "1"
                    ],
                    [
                      "M\u00fcller",
                      "Mu\u0308ller",
                      "M\u00fcller",
                      "Mu\u0308ller",
                      "M\u00fcller",
                      "M\u00fcller",
                      "fianc\u00e9",
                      "",
                      "\u00c9ve M\u00fcller",
                      "m\u00fcller",
                      "mu\u0308ller",
                      "1",
                      "False",
                      "\u00c9",
                      "True",
                      "\u00c9ve",
                      "2"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new NormalizeCornersQuery().Result).TextValue
        );
    }
}
