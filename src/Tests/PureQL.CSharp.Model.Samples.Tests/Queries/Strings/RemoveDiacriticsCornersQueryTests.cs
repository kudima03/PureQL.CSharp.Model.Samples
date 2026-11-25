using PureQL.CSharp.Model.Samples.Queries.Strings;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Strings;

public sealed record RemoveDiacriticsCornersQueryTests
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
                      "alias": "removed_twice",
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
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
                      "alias": "of_a_literal",
                      "expression": {
                        "operator": "removeDiacritics",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "type": {
                            "name": "string"
                          },
                          "value": "Cr\u00e8me Br\u00fbl\u00e9e"
                        }
                      }
                    },
                    {
                      "alias": "of_a_nullable_literal",
                      "expression": {
                        "operator": "removeDiacritics",
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
                        }
                      }
                    },
                    {
                      "alias": "of_a_concat",
                      "expression": {
                        "operator": "removeDiacritics",
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
                        }
                      }
                    },
                    {
                      "alias": "fold_then_remove",
                      "expression": {
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
                    },
                    {
                      "alias": "remove_then_fold",
                      "expression": {
                        "operator": "foldCase",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
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
                      "alias": "of_a_decomposed_string",
                      "expression": {
                        "operator": "removeDiacritics",
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
                      }
                    },
                    {
                      "alias": "is_already_composed",
                      "expression": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
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
                        },
                        "right": {
                          "operator": "normalize",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
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
                          },
                          "form": "NFC"
                        }
                      }
                    },
                    {
                      "alias": "marks_removed",
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
                              },
                              "form": "NFD"
                            }
                          }
                        ]
                      }
                    },
                    {
                      "alias": "starts_with_needle",
                      "expression": {
                        "operator": "startsWith",
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
                              "value": "Lef\u00c8"
                            }
                          }
                        }
                      }
                    },
                    {
                      "alias": "ascii_only",
                      "expression": {
                        "operator": "matchesRegex",
                        "type": {
                          "name": "boolean"
                        },
                        "operand": {
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
                        },
                        "pattern": {
                          "type": {
                            "name": "string"
                          },
                          "value": "^[A-Z][a-z]*$"
                        }
                      }
                    },
                    {
                      "alias": "initial",
                      "expression": {
                        "operator": "upper",
                        "type": {
                          "name": "string"
                        },
                        "operand": {
                          "operator": "substring",
                          "type": {
                            "name": "string"
                          },
                          "operand": {
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
                            }
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
                            "operator": "removeDiacritics",
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
                            }
                          },
                          {
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
                            }
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
                          ],
                          "orderBy": [
                            {
                              "expression": {
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
                                  }
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
            new QueryJson(new RemoveDiacriticsCornersQuery().Value).TextValue
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
                      "name": "removed_twice",
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
                      "name": "fold_then_remove",
                      "type": "string"
                    },
                    {
                      "name": "remove_then_fold",
                      "type": "string"
                    },
                    {
                      "name": "of_a_decomposed_string",
                      "type": "string"
                    },
                    {
                      "name": "is_already_composed",
                      "type": "bool"
                    },
                    {
                      "name": "marks_removed",
                      "type": "long"
                    },
                    {
                      "name": "starts_with_needle",
                      "type": "bool"
                    },
                    {
                      "name": "ascii_only",
                      "type": "bool"
                    },
                    {
                      "name": "initial",
                      "type": "string"
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
                      "Lefevre",
                      "Creme Brulee",
                      "",
                      "Ann Lefevre",
                      "lefevre",
                      "lefevre",
                      "Lefevre",
                      "True",
                      "1",
                      "True",
                      "True",
                      "A",
                      "Zoe",
                      "1"
                    ],
                    [
                      "LEFEVRE",
                      "Creme Brulee",
                      "",
                      "Bob LEFEVRE",
                      "lefevre",
                      "lefevre",
                      "LEFEVRE",
                      "True",
                      "1",
                      "True",
                      "False",
                      "B",
                      "Bob",
                      "3"
                    ],
                    [
                      "Lefevre",
                      "Creme Brulee",
                      "",
                      "ANN Lefevre",
                      "lefevre",
                      "lefevre",
                      "Lefevre",
                      "True",
                      "1",
                      "True",
                      "True",
                      "A",
                      "Zoe",
                      "2"
                    ],
                    [
                      "Muller",
                      "Creme Brulee",
                      "",
                      "Dan Muller",
                      "muller",
                      "muller",
                      "Muller",
                      "True",
                      "1",
                      "False",
                      "True",
                      "D",
                      "Dan",
                      "1"
                    ],
                    [
                      "Muller",
                      "Creme Brulee",
                      "",
                      "Eve Muller",
                      "muller",
                      "muller",
                      "Muller",
                      "True",
                      "1",
                      "False",
                      "True",
                      "E",
                      "Eve",
                      "2"
                    ],
                    [
                      "MULLER",
                      "Creme Brulee",
                      "",
                      "Fay MULLER",
                      "muller",
                      "muller",
                      "MULLER",
                      "True",
                      "0",
                      "False",
                      "False",
                      "F",
                      "Fay",
                      "3"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new RemoveDiacriticsCornersQuery().Result).TextValue
        );
    }
}
