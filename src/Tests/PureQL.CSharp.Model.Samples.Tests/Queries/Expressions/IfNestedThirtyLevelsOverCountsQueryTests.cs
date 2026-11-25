using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record IfNestedThirtyLevelsOverCountsQueryTests
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
                  "select": [
                    {
                      "alias": "tier",
                      "expression": {
                        "operator": "if",
                        "type": {
                          "name": "integer"
                        },
                        "condition": {
                          "operator": "greaterThan",
                          "type": {
                            "name": "boolean"
                          },
                          "left": {
                            "operator": "count",
                            "type": {
                              "name": "integer"
                            }
                          },
                          "right": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 26
                          }
                        },
                        "then": {
                          "type": {
                            "name": "integer"
                          },
                          "value": 0
                        },
                        "else": {
                          "operator": "if",
                          "type": {
                            "name": "integer"
                          },
                          "condition": {
                            "operator": "greaterThan",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "count",
                              "type": {
                                "name": "integer"
                              }
                            },
                            "right": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 25
                            }
                          },
                          "then": {
                            "type": {
                              "name": "integer"
                            },
                            "value": 1
                          },
                          "else": {
                            "operator": "if",
                            "type": {
                              "name": "integer"
                            },
                            "condition": {
                              "operator": "greaterThan",
                              "type": {
                                "name": "boolean"
                              },
                              "left": {
                                "operator": "count",
                                "type": {
                                  "name": "integer"
                                }
                              },
                              "right": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 24
                              }
                            },
                            "then": {
                              "type": {
                                "name": "integer"
                              },
                              "value": 2
                            },
                            "else": {
                              "operator": "if",
                              "type": {
                                "name": "integer"
                              },
                              "condition": {
                                "operator": "greaterThan",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "operator": "count",
                                  "type": {
                                    "name": "integer"
                                  }
                                },
                                "right": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 23
                                }
                              },
                              "then": {
                                "type": {
                                  "name": "integer"
                                },
                                "value": 3
                              },
                              "else": {
                                "operator": "if",
                                "type": {
                                  "name": "integer"
                                },
                                "condition": {
                                  "operator": "greaterThan",
                                  "type": {
                                    "name": "boolean"
                                  },
                                  "left": {
                                    "operator": "count",
                                    "type": {
                                      "name": "integer"
                                    }
                                  },
                                  "right": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 22
                                  }
                                },
                                "then": {
                                  "type": {
                                    "name": "integer"
                                  },
                                  "value": 4
                                },
                                "else": {
                                  "operator": "if",
                                  "type": {
                                    "name": "integer"
                                  },
                                  "condition": {
                                    "operator": "greaterThan",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "operator": "count",
                                      "type": {
                                        "name": "integer"
                                      }
                                    },
                                    "right": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 21
                                    }
                                  },
                                  "then": {
                                    "type": {
                                      "name": "integer"
                                    },
                                    "value": 5
                                  },
                                  "else": {
                                    "operator": "if",
                                    "type": {
                                      "name": "integer"
                                    },
                                    "condition": {
                                      "operator": "greaterThan",
                                      "type": {
                                        "name": "boolean"
                                      },
                                      "left": {
                                        "operator": "count",
                                        "type": {
                                          "name": "integer"
                                        }
                                      },
                                      "right": {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 20
                                      }
                                    },
                                    "then": {
                                      "type": {
                                        "name": "integer"
                                      },
                                      "value": 6
                                    },
                                    "else": {
                                      "operator": "if",
                                      "type": {
                                        "name": "integer"
                                      },
                                      "condition": {
                                        "operator": "greaterThan",
                                        "type": {
                                          "name": "boolean"
                                        },
                                        "left": {
                                          "operator": "count",
                                          "type": {
                                            "name": "integer"
                                          }
                                        },
                                        "right": {
                                          "type": {
                                            "name": "integer"
                                          },
                                          "value": 19
                                        }
                                      },
                                      "then": {
                                        "type": {
                                          "name": "integer"
                                        },
                                        "value": 7
                                      },
                                      "else": {
                                        "operator": "if",
                                        "type": {
                                          "name": "integer"
                                        },
                                        "condition": {
                                          "operator": "greaterThan",
                                          "type": {
                                            "name": "boolean"
                                          },
                                          "left": {
                                            "operator": "count",
                                            "type": {
                                              "name": "integer"
                                            }
                                          },
                                          "right": {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": 18
                                          }
                                        },
                                        "then": {
                                          "type": {
                                            "name": "integer"
                                          },
                                          "value": 8
                                        },
                                        "else": {
                                          "operator": "if",
                                          "type": {
                                            "name": "integer"
                                          },
                                          "condition": {
                                            "operator": "greaterThan",
                                            "type": {
                                              "name": "boolean"
                                            },
                                            "left": {
                                              "operator": "count",
                                              "type": {
                                                "name": "integer"
                                              }
                                            },
                                            "right": {
                                              "type": {
                                                "name": "integer"
                                              },
                                              "value": 17
                                            }
                                          },
                                          "then": {
                                            "type": {
                                              "name": "integer"
                                            },
                                            "value": 9
                                          },
                                          "else": {
                                            "operator": "if",
                                            "type": {
                                              "name": "integer"
                                            },
                                            "condition": {
                                              "operator": "greaterThan",
                                              "type": {
                                                "name": "boolean"
                                              },
                                              "left": {
                                                "operator": "count",
                                                "type": {
                                                  "name": "integer"
                                                }
                                              },
                                              "right": {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 16
                                              }
                                            },
                                            "then": {
                                              "type": {
                                                "name": "integer"
                                              },
                                              "value": 10
                                            },
                                            "else": {
                                              "operator": "if",
                                              "type": {
                                                "name": "integer"
                                              },
                                              "condition": {
                                                "operator": "greaterThan",
                                                "type": {
                                                  "name": "boolean"
                                                },
                                                "left": {
                                                  "operator": "count",
                                                  "type": {
                                                    "name": "integer"
                                                  }
                                                },
                                                "right": {
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "value": 15
                                                }
                                              },
                                              "then": {
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "value": 11
                                              },
                                              "else": {
                                                "operator": "if",
                                                "type": {
                                                  "name": "integer"
                                                },
                                                "condition": {
                                                  "operator": "greaterThan",
                                                  "type": {
                                                    "name": "boolean"
                                                  },
                                                  "left": {
                                                    "operator": "count",
                                                    "type": {
                                                      "name": "integer"
                                                    }
                                                  },
                                                  "right": {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 14
                                                  }
                                                },
                                                "then": {
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "value": 12
                                                },
                                                "else": {
                                                  "operator": "if",
                                                  "type": {
                                                    "name": "integer"
                                                  },
                                                  "condition": {
                                                    "operator": "greaterThan",
                                                    "type": {
                                                      "name": "boolean"
                                                    },
                                                    "left": {
                                                      "operator": "count",
                                                      "type": {
                                                        "name": "integer"
                                                      }
                                                    },
                                                    "right": {
                                                      "type": {
                                                        "name": "integer"
                                                      },
                                                      "value": 13
                                                    }
                                                  },
                                                  "then": {
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "value": 13
                                                  },
                                                  "else": {
                                                    "operator": "if",
                                                    "type": {
                                                      "name": "integer"
                                                    },
                                                    "condition": {
                                                      "operator": "greaterThan",
                                                      "type": {
                                                        "name": "boolean"
                                                      },
                                                      "left": {
                                                        "operator": "count",
                                                        "type": {
                                                          "name": "integer"
                                                        }
                                                      },
                                                      "right": {
                                                        "type": {
                                                          "name": "integer"
                                                        },
                                                        "value": 12
                                                      }
                                                    },
                                                    "then": {
                                                      "type": {
                                                        "name": "integer"
                                                      },
                                                      "value": 14
                                                    },
                                                    "else": {
                                                      "operator": "if",
                                                      "type": {
                                                        "name": "integer"
                                                      },
                                                      "condition": {
                                                        "operator": "greaterThan",
                                                        "type": {
                                                          "name": "boolean"
                                                        },
                                                        "left": {
                                                          "operator": "count",
                                                          "type": {
                                                            "name": "integer"
                                                          }
                                                        },
                                                        "right": {
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "value": 11
                                                        }
                                                      },
                                                      "then": {
                                                        "type": {
                                                          "name": "integer"
                                                        },
                                                        "value": 15
                                                      },
                                                      "else": {
                                                        "operator": "if",
                                                        "type": {
                                                          "name": "integer"
                                                        },
                                                        "condition": {
                                                          "operator": "greaterThan",
                                                          "type": {
                                                            "name": "boolean"
                                                          },
                                                          "left": {
                                                            "operator": "count",
                                                            "type": {
                                                              "name": "integer"
                                                            }
                                                          },
                                                          "right": {
                                                            "type": {
                                                              "name": "integer"
                                                            },
                                                            "value": 10
                                                          }
                                                        },
                                                        "then": {
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "value": 16
                                                        },
                                                        "else": {
                                                          "operator": "if",
                                                          "type": {
                                                            "name": "integer"
                                                          },
                                                          "condition": {
                                                            "operator": "greaterThan",
                                                            "type": {
                                                              "name": "boolean"
                                                            },
                                                            "left": {
                                                              "operator": "count",
                                                              "type": {
                                                                "name": "integer"
                                                              }
                                                            },
                                                            "right": {
                                                              "type": {
                                                                "name": "integer"
                                                              },
                                                              "value": 9
                                                            }
                                                          },
                                                          "then": {
                                                            "type": {
                                                              "name": "integer"
                                                            },
                                                            "value": 17
                                                          },
                                                          "else": {
                                                            "operator": "if",
                                                            "type": {
                                                              "name": "integer"
                                                            },
                                                            "condition": {
                                                              "operator": "greaterThan",
                                                              "type": {
                                                                "name": "boolean"
                                                              },
                                                              "left": {
                                                                "operator": "count",
                                                                "type": {
                                                                  "name": "integer"
                                                                }
                                                              },
                                                              "right": {
                                                                "type": {
                                                                  "name": "integer"
                                                                },
                                                                "value": 8
                                                              }
                                                            },
                                                            "then": {
                                                              "type": {
                                                                "name": "integer"
                                                              },
                                                              "value": 18
                                                            },
                                                            "else": {
                                                              "operator": "if",
                                                              "type": {
                                                                "name": "integer"
                                                              },
                                                              "condition": {
                                                                "operator": "greaterThan",
                                                                "type": {
                                                                  "name": "boolean"
                                                                },
                                                                "left": {
                                                                  "operator": "count",
                                                                  "type": {
                                                                    "name": "integer"
                                                                  }
                                                                },
                                                                "right": {
                                                                  "type": {
                                                                    "name": "integer"
                                                                  },
                                                                  "value": 7
                                                                }
                                                              },
                                                              "then": {
                                                                "type": {
                                                                  "name": "integer"
                                                                },
                                                                "value": 19
                                                              },
                                                              "else": {
                                                                "operator": "if",
                                                                "type": {
                                                                  "name": "integer"
                                                                },
                                                                "condition": {
                                                                  "operator": "greaterThan",
                                                                  "type": {
                                                                    "name": "boolean"
                                                                  },
                                                                  "left": {
                                                                    "operator": "count",
                                                                    "type": {
                                                                      "name": "integer"
                                                                    }
                                                                  },
                                                                  "right": {
                                                                    "type": {
                                                                      "name": "integer"
                                                                    },
                                                                    "value": 6
                                                                  }
                                                                },
                                                                "then": {
                                                                  "type": {
                                                                    "name": "integer"
                                                                  },
                                                                  "value": 20
                                                                },
                                                                "else": {
                                                                  "operator": "if",
                                                                  "type": {
                                                                    "name": "integer"
                                                                  },
                                                                  "condition": {
                                                                    "operator": "greaterThan",
                                                                    "type": {
                                                                      "name": "boolean"
                                                                    },
                                                                    "left": {
                                                                      "operator": "count",
                                                                      "type": {
                                                                        "name": "integer"
                                                                      }
                                                                    },
                                                                    "right": {
                                                                      "type": {
                                                                        "name": "integer"
                                                                      },
                                                                      "value": 5
                                                                    }
                                                                  },
                                                                  "then": {
                                                                    "type": {
                                                                      "name": "integer"
                                                                    },
                                                                    "value": 21
                                                                  },
                                                                  "else": {
                                                                    "operator": "if",
                                                                    "type": {
                                                                      "name": "integer"
                                                                    },
                                                                    "condition": {
                                                                      "operator": "greaterThan",
                                                                      "type": {
                                                                        "name": "boolean"
                                                                      },
                                                                      "left": {
                                                                        "operator": "count",
                                                                        "type": {
                                                                          "name": "integer"
                                                                        }
                                                                      },
                                                                      "right": {
                                                                        "type": {
                                                                          "name": "integer"
                                                                        },
                                                                        "value": 4
                                                                      }
                                                                    },
                                                                    "then": {
                                                                      "type": {
                                                                        "name": "integer"
                                                                      },
                                                                      "value": 22
                                                                    },
                                                                    "else": {
                                                                      "operator": "if",
                                                                      "type": {
                                                                        "name": "integer"
                                                                      },
                                                                      "condition": {
                                                                        "operator": "greaterThan",
                                                                        "type": {
                                                                          "name": "boolean"
                                                                        },
                                                                        "left": {
                                                                          "operator": "count",
                                                                          "type": {
                                                                            "name": "integer"
                                                                          }
                                                                        },
                                                                        "right": {
                                                                          "type": {
                                                                            "name": "integer"
                                                                          },
                                                                          "value": 3
                                                                        }
                                                                      },
                                                                      "then": {
                                                                        "type": {
                                                                          "name": "integer"
                                                                        },
                                                                        "value": 23
                                                                      },
                                                                      "else": {
                                                                        "operator": "if",
                                                                        "type": {
                                                                          "name": "integer"
                                                                        },
                                                                        "condition": {
                                                                          "operator": "greaterThan",
                                                                          "type": {
                                                                            "name": "boolean"
                                                                          },
                                                                          "left": {
                                                                            "operator": "count",
                                                                            "type": {
                                                                              "name": "integer"
                                                                            }
                                                                          },
                                                                          "right": {
                                                                            "type": {
                                                                              "name": "integer"
                                                                            },
                                                                            "value": 2
                                                                          }
                                                                        },
                                                                        "then": {
                                                                          "type": {
                                                                            "name": "integer"
                                                                          },
                                                                          "value": 24
                                                                        },
                                                                        "else": {
                                                                          "operator": "if",
                                                                          "type": {
                                                                            "name": "integer"
                                                                          },
                                                                          "condition": {
                                                                            "operator": "greaterThan",
                                                                            "type": {
                                                                              "name": "boolean"
                                                                            },
                                                                            "left": {
                                                                              "operator": "count",
                                                                              "type": {
                                                                                "name": "integer"
                                                                              }
                                                                            },
                                                                            "right": {
                                                                              "type": {
                                                                                "name": "integer"
                                                                              },
                                                                              "value": 1
                                                                            }
                                                                          },
                                                                          "then": {
                                                                            "type": {
                                                                              "name": "integer"
                                                                            },
                                                                            "value": 25
                                                                          },
                                                                          "else": {
                                                                            "operator": "if",
                                                                            "type": {
                                                                              "name": "integer"
                                                                            },
                                                                            "condition": {
                                                                              "operator": "greaterThan",
                                                                              "type": {
                                                                                "name": "boolean"
                                                                              },
                                                                              "left": {
                                                                                "operator": "count",
                                                                                "type": {
                                                                                  "name": "integer"
                                                                                }
                                                                              },
                                                                              "right": {
                                                                                "type": {
                                                                                  "name": "integer"
                                                                                },
                                                                                "value": 0
                                                                              }
                                                                            },
                                                                            "then": {
                                                                              "type": {
                                                                                "name": "integer"
                                                                              },
                                                                              "value": 26
                                                                            },
                                                                            "else": {
                                                                              "operator": "if",
                                                                              "type": {
                                                                                "name": "integer"
                                                                              },
                                                                              "condition": {
                                                                                "operator": "greaterThan",
                                                                                "type": {
                                                                                  "name": "boolean"
                                                                                },
                                                                                "left": {
                                                                                  "operator": "count",
                                                                                  "type": {
                                                                                    "name": "integer"
                                                                                  }
                                                                                },
                                                                                "right": {
                                                                                  "type": {
                                                                                    "name": "integer"
                                                                                  },
                                                                                  "value": -1
                                                                                }
                                                                              },
                                                                              "then": {
                                                                                "type": {
                                                                                  "name": "integer"
                                                                                },
                                                                                "value": 27
                                                                              },
                                                                              "else": {
                                                                                "operator": "if",
                                                                                "type": {
                                                                                  "name": "integer"
                                                                                },
                                                                                "condition": {
                                                                                  "operator": "greaterThan",
                                                                                  "type": {
                                                                                    "name": "boolean"
                                                                                  },
                                                                                  "left": {
                                                                                    "operator": "count",
                                                                                    "type": {
                                                                                      "name": "integer"
                                                                                    }
                                                                                  },
                                                                                  "right": {
                                                                                    "type": {
                                                                                      "name": "integer"
                                                                                    },
                                                                                    "value": -2
                                                                                  }
                                                                                },
                                                                                "then": {
                                                                                  "type": {
                                                                                    "name": "integer"
                                                                                  },
                                                                                  "value": 28
                                                                                },
                                                                                "else": {
                                                                                  "operator": "if",
                                                                                  "type": {
                                                                                    "name": "integer"
                                                                                  },
                                                                                  "condition": {
                                                                                    "operator": "greaterThan",
                                                                                    "type": {
                                                                                      "name": "boolean"
                                                                                    },
                                                                                    "left": {
                                                                                      "operator": "count",
                                                                                      "type": {
                                                                                        "name": "integer"
                                                                                      }
                                                                                    },
                                                                                    "right": {
                                                                                      "type": {
                                                                                        "name": "integer"
                                                                                      },
                                                                                      "value": -3
                                                                                    }
                                                                                  },
                                                                                  "then": {
                                                                                    "type": {
                                                                                      "name": "integer"
                                                                                    },
                                                                                    "value": 29
                                                                                  },
                                                                                  "else": {
                                                                                    "type": {
                                                                                      "name": "integer"
                                                                                    },
                                                                                    "value": 99
                                                                                  }
                                                                                }
                                                                              }
                                                                            }
                                                                          }
                                                                        }
                                                                      }
                                                                    }
                                                                  }
                                                                }
                                                              }
                                                            }
                                                          }
                                                        }
                                                      }
                                                    }
                                                  }
                                                }
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
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
            new QueryJson(new IfNestedThirtyLevelsOverCountsQuery().Value).TextValue
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
                      "name": "tier",
                      "type": "long"
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "21"
                    ],
                    [
                      "21"
                    ],
                    [
                      "21"
                    ],
                    [
                      "21"
                    ],
                    [
                      "21"
                    ],
                    [
                      "21"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new IfNestedThirtyLevelsOverCountsQuery().Result).TextValue
        );
    }
}
