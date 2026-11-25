using PureQL.CSharp.Model.Samples.Queries.Expressions;

namespace PureQL.CSharp.Model.Samples.Tests.Queries.Expressions;

public sealed record DeeplyNestedIfAndEqualQueryTests
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
                  "joins": [
                    {
                      "kind": "inner",
                      "entity": "schema_with_foreign_keys.users",
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
                    "operator": "equal",
                    "type": {
                      "name": "boolean"
                    },
                    "left": {
                      "operator": "if",
                      "type": {
                        "name": "uuid"
                      },
                      "condition": {
                        "operator": "equal",
                        "type": {
                          "name": "boolean"
                        },
                        "left": {
                          "operator": "if",
                          "type": {
                            "name": "uuid"
                          },
                          "condition": {
                            "operator": "equal",
                            "type": {
                              "name": "boolean"
                            },
                            "left": {
                              "operator": "if",
                              "type": {
                                "name": "uuid"
                              },
                              "condition": {
                                "operator": "equal",
                                "type": {
                                  "name": "boolean"
                                },
                                "left": {
                                  "operator": "if",
                                  "type": {
                                    "name": "uuid"
                                  },
                                  "condition": {
                                    "operator": "equal",
                                    "type": {
                                      "name": "boolean"
                                    },
                                    "left": {
                                      "operator": "if",
                                      "type": {
                                        "name": "uuid"
                                      },
                                      "condition": {
                                        "operator": "equal",
                                        "type": {
                                          "name": "boolean"
                                        },
                                        "left": {
                                          "operator": "if",
                                          "type": {
                                            "name": "uuid"
                                          },
                                          "condition": {
                                            "operator": "equal",
                                            "type": {
                                              "name": "boolean"
                                            },
                                            "left": {
                                              "operator": "if",
                                              "type": {
                                                "name": "uuid"
                                              },
                                              "condition": {
                                                "operator": "equal",
                                                "type": {
                                                  "name": "boolean"
                                                },
                                                "left": {
                                                  "operator": "if",
                                                  "type": {
                                                    "name": "uuid"
                                                  },
                                                  "condition": {
                                                    "operator": "equal",
                                                    "type": {
                                                      "name": "boolean"
                                                    },
                                                    "left": {
                                                      "operator": "if",
                                                      "type": {
                                                        "name": "uuid"
                                                      },
                                                      "condition": {
                                                        "operator": "equal",
                                                        "type": {
                                                          "name": "boolean"
                                                        },
                                                        "left": {
                                                          "operator": "if",
                                                          "type": {
                                                            "name": "uuid"
                                                          },
                                                          "condition": {
                                                            "operator": "equal",
                                                            "type": {
                                                              "name": "boolean"
                                                            },
                                                            "left": {
                                                              "operator": "if",
                                                              "type": {
                                                                "name": "uuid"
                                                              },
                                                              "condition": {
                                                                "operator": "equal",
                                                                "type": {
                                                                  "name": "boolean"
                                                                },
                                                                "left": {
                                                                  "operator": "if",
                                                                  "type": {
                                                                    "name": "uuid"
                                                                  },
                                                                  "condition": {
                                                                    "operator": "equal",
                                                                    "type": {
                                                                      "name": "boolean"
                                                                    },
                                                                    "left": {
                                                                      "operator": "if",
                                                                      "type": {
                                                                        "name": "uuid"
                                                                      },
                                                                      "condition": {
                                                                        "operator": "equal",
                                                                        "type": {
                                                                          "name": "boolean"
                                                                        },
                                                                        "left": {
                                                                          "operator": "if",
                                                                          "type": {
                                                                            "name": "uuid"
                                                                          },
                                                                          "condition": {
                                                                            "operator": "equal",
                                                                            "type": {
                                                                              "name": "boolean"
                                                                            },
                                                                            "left": {
                                                                              "operator": "if",
                                                                              "type": {
                                                                                "name": "uuid"
                                                                              },
                                                                              "condition": {
                                                                                "operator": "equal",
                                                                                "type": {
                                                                                  "name": "boolean"
                                                                                },
                                                                                "left": {
                                                                                  "operator": "if",
                                                                                  "type": {
                                                                                    "name": "uuid"
                                                                                  },
                                                                                  "condition": {
                                                                                    "operator": "equal",
                                                                                    "type": {
                                                                                      "name": "boolean"
                                                                                    },
                                                                                    "left": {
                                                                                      "operator": "if",
                                                                                      "type": {
                                                                                        "name": "uuid"
                                                                                      },
                                                                                      "condition": {
                                                                                        "operator": "equal",
                                                                                        "type": {
                                                                                          "name": "boolean"
                                                                                        },
                                                                                        "left": {
                                                                                          "operator": "if",
                                                                                          "type": {
                                                                                            "name": "uuid"
                                                                                          },
                                                                                          "condition": {
                                                                                            "operator": "equal",
                                                                                            "type": {
                                                                                              "name": "boolean"
                                                                                            },
                                                                                            "left": {
                                                                                              "operator": "if",
                                                                                              "type": {
                                                                                                "name": "uuid"
                                                                                              },
                                                                                              "condition": {
                                                                                                "operator": "equal",
                                                                                                "type": {
                                                                                                  "name": "boolean"
                                                                                                },
                                                                                                "left": {
                                                                                                  "operator": "if",
                                                                                                  "type": {
                                                                                                    "name": "uuid"
                                                                                                  },
                                                                                                  "condition": {
                                                                                                    "operator": "equal",
                                                                                                    "type": {
                                                                                                      "name": "boolean"
                                                                                                    },
                                                                                                    "left": {
                                                                                                      "operator": "if",
                                                                                                      "type": {
                                                                                                        "name": "uuid"
                                                                                                      },
                                                                                                      "condition": {
                                                                                                        "operator": "equal",
                                                                                                        "type": {
                                                                                                          "name": "boolean"
                                                                                                        },
                                                                                                        "left": {
                                                                                                          "operator": "if",
                                                                                                          "type": {
                                                                                                            "name": "uuid"
                                                                                                          },
                                                                                                          "condition": {
                                                                                                            "operator": "equal",
                                                                                                            "type": {
                                                                                                              "name": "boolean"
                                                                                                            },
                                                                                                            "left": {
                                                                                                              "operator": "if",
                                                                                                              "type": {
                                                                                                                "name": "uuid"
                                                                                                              },
                                                                                                              "condition": {
                                                                                                                "operator": "equal",
                                                                                                                "type": {
                                                                                                                  "name": "boolean"
                                                                                                                },
                                                                                                                "left": {
                                                                                                                  "operator": "if",
                                                                                                                  "type": {
                                                                                                                    "name": "uuid"
                                                                                                                  },
                                                                                                                  "condition": {
                                                                                                                    "operator": "equal",
                                                                                                                    "type": {
                                                                                                                      "name": "boolean"
                                                                                                                    },
                                                                                                                    "left": {
                                                                                                                      "operator": "if",
                                                                                                                      "type": {
                                                                                                                        "name": "uuid"
                                                                                                                      },
                                                                                                                      "condition": {
                                                                                                                        "operator": "equal",
                                                                                                                        "type": {
                                                                                                                          "name": "boolean"
                                                                                                                        },
                                                                                                                        "left": {
                                                                                                                          "operator": "if",
                                                                                                                          "type": {
                                                                                                                            "name": "uuid"
                                                                                                                          },
                                                                                                                          "condition": {
                                                                                                                            "operator": "equal",
                                                                                                                            "type": {
                                                                                                                              "name": "boolean"
                                                                                                                            },
                                                                                                                            "left": {
                                                                                                                              "operator": "if",
                                                                                                                              "type": {
                                                                                                                                "name": "uuid"
                                                                                                                              },
                                                                                                                              "condition": {
                                                                                                                                "operator": "equal",
                                                                                                                                "type": {
                                                                                                                                  "name": "boolean"
                                                                                                                                },
                                                                                                                                "left": {
                                                                                                                                  "operator": "if",
                                                                                                                                  "type": {
                                                                                                                                    "name": "uuid"
                                                                                                                                  },
                                                                                                                                  "condition": {
                                                                                                                                    "operator": "equal",
                                                                                                                                    "type": {
                                                                                                                                      "name": "boolean"
                                                                                                                                    },
                                                                                                                                    "left": {
                                                                                                                                      "operator": "if",
                                                                                                                                      "type": {
                                                                                                                                        "name": "uuid"
                                                                                                                                      },
                                                                                                                                      "condition": {
                                                                                                                                        "operator": "equal",
                                                                                                                                        "type": {
                                                                                                                                          "name": "boolean"
                                                                                                                                        },
                                                                                                                                        "left": {
                                                                                                                                          "operator": "if",
                                                                                                                                          "type": {
                                                                                                                                            "name": "uuid"
                                                                                                                                          },
                                                                                                                                          "condition": {
                                                                                                                                            "source": "schema_with_foreign_keys.users",
                                                                                                                                            "field": "user_active",
                                                                                                                                            "type": {
                                                                                                                                              "name": "boolean"
                                                                                                                                            }
                                                                                                                                          },
                                                                                                                                          "then": {
                                                                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                                                                            "field": "order_id",
                                                                                                                                            "type": {
                                                                                                                                              "name": "uuid"
                                                                                                                                            }
                                                                                                                                          },
                                                                                                                                          "else": {
                                                                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                                                                            "field": "order_user_id",
                                                                                                                                            "type": {
                                                                                                                                              "name": "uuid"
                                                                                                                                            }
                                                                                                                                          }
                                                                                                                                        },
                                                                                                                                        "right": {
                                                                                                                                          "source": "schema_with_foreign_keys.orders",
                                                                                                                                          "field": "order_id",
                                                                                                                                          "type": {
                                                                                                                                            "name": "uuid"
                                                                                                                                          }
                                                                                                                                        }
                                                                                                                                      },
                                                                                                                                      "then": {
                                                                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                                                                        "field": "order_id",
                                                                                                                                        "type": {
                                                                                                                                          "name": "uuid"
                                                                                                                                        }
                                                                                                                                      },
                                                                                                                                      "else": {
                                                                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                                                                        "field": "order_user_id",
                                                                                                                                        "type": {
                                                                                                                                          "name": "uuid"
                                                                                                                                        }
                                                                                                                                      }
                                                                                                                                    },
                                                                                                                                    "right": {
                                                                                                                                      "source": "schema_with_foreign_keys.orders",
                                                                                                                                      "field": "order_id",
                                                                                                                                      "type": {
                                                                                                                                        "name": "uuid"
                                                                                                                                      }
                                                                                                                                    }
                                                                                                                                  },
                                                                                                                                  "then": {
                                                                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                                                                    "field": "order_id",
                                                                                                                                    "type": {
                                                                                                                                      "name": "uuid"
                                                                                                                                    }
                                                                                                                                  },
                                                                                                                                  "else": {
                                                                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                                                                    "field": "order_user_id",
                                                                                                                                    "type": {
                                                                                                                                      "name": "uuid"
                                                                                                                                    }
                                                                                                                                  }
                                                                                                                                },
                                                                                                                                "right": {
                                                                                                                                  "source": "schema_with_foreign_keys.orders",
                                                                                                                                  "field": "order_id",
                                                                                                                                  "type": {
                                                                                                                                    "name": "uuid"
                                                                                                                                  }
                                                                                                                                }
                                                                                                                              },
                                                                                                                              "then": {
                                                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                                                "field": "order_id",
                                                                                                                                "type": {
                                                                                                                                  "name": "uuid"
                                                                                                                                }
                                                                                                                              },
                                                                                                                              "else": {
                                                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                                                "field": "order_user_id",
                                                                                                                                "type": {
                                                                                                                                  "name": "uuid"
                                                                                                                                }
                                                                                                                              }
                                                                                                                            },
                                                                                                                            "right": {
                                                                                                                              "source": "schema_with_foreign_keys.orders",
                                                                                                                              "field": "order_id",
                                                                                                                              "type": {
                                                                                                                                "name": "uuid"
                                                                                                                              }
                                                                                                                            }
                                                                                                                          },
                                                                                                                          "then": {
                                                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                                                            "field": "order_id",
                                                                                                                            "type": {
                                                                                                                              "name": "uuid"
                                                                                                                            }
                                                                                                                          },
                                                                                                                          "else": {
                                                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                                                            "field": "order_user_id",
                                                                                                                            "type": {
                                                                                                                              "name": "uuid"
                                                                                                                            }
                                                                                                                          }
                                                                                                                        },
                                                                                                                        "right": {
                                                                                                                          "source": "schema_with_foreign_keys.orders",
                                                                                                                          "field": "order_id",
                                                                                                                          "type": {
                                                                                                                            "name": "uuid"
                                                                                                                          }
                                                                                                                        }
                                                                                                                      },
                                                                                                                      "then": {
                                                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                                                        "field": "order_id",
                                                                                                                        "type": {
                                                                                                                          "name": "uuid"
                                                                                                                        }
                                                                                                                      },
                                                                                                                      "else": {
                                                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                                                        "field": "order_user_id",
                                                                                                                        "type": {
                                                                                                                          "name": "uuid"
                                                                                                                        }
                                                                                                                      }
                                                                                                                    },
                                                                                                                    "right": {
                                                                                                                      "source": "schema_with_foreign_keys.orders",
                                                                                                                      "field": "order_id",
                                                                                                                      "type": {
                                                                                                                        "name": "uuid"
                                                                                                                      }
                                                                                                                    }
                                                                                                                  },
                                                                                                                  "then": {
                                                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                                                    "field": "order_id",
                                                                                                                    "type": {
                                                                                                                      "name": "uuid"
                                                                                                                    }
                                                                                                                  },
                                                                                                                  "else": {
                                                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                                                    "field": "order_user_id",
                                                                                                                    "type": {
                                                                                                                      "name": "uuid"
                                                                                                                    }
                                                                                                                  }
                                                                                                                },
                                                                                                                "right": {
                                                                                                                  "source": "schema_with_foreign_keys.orders",
                                                                                                                  "field": "order_id",
                                                                                                                  "type": {
                                                                                                                    "name": "uuid"
                                                                                                                  }
                                                                                                                }
                                                                                                              },
                                                                                                              "then": {
                                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                                "field": "order_id",
                                                                                                                "type": {
                                                                                                                  "name": "uuid"
                                                                                                                }
                                                                                                              },
                                                                                                              "else": {
                                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                                "field": "order_user_id",
                                                                                                                "type": {
                                                                                                                  "name": "uuid"
                                                                                                                }
                                                                                                              }
                                                                                                            },
                                                                                                            "right": {
                                                                                                              "source": "schema_with_foreign_keys.orders",
                                                                                                              "field": "order_id",
                                                                                                              "type": {
                                                                                                                "name": "uuid"
                                                                                                              }
                                                                                                            }
                                                                                                          },
                                                                                                          "then": {
                                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                                            "field": "order_id",
                                                                                                            "type": {
                                                                                                              "name": "uuid"
                                                                                                            }
                                                                                                          },
                                                                                                          "else": {
                                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                                            "field": "order_user_id",
                                                                                                            "type": {
                                                                                                              "name": "uuid"
                                                                                                            }
                                                                                                          }
                                                                                                        },
                                                                                                        "right": {
                                                                                                          "source": "schema_with_foreign_keys.orders",
                                                                                                          "field": "order_id",
                                                                                                          "type": {
                                                                                                            "name": "uuid"
                                                                                                          }
                                                                                                        }
                                                                                                      },
                                                                                                      "then": {
                                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                                        "field": "order_id",
                                                                                                        "type": {
                                                                                                          "name": "uuid"
                                                                                                        }
                                                                                                      },
                                                                                                      "else": {
                                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                                        "field": "order_user_id",
                                                                                                        "type": {
                                                                                                          "name": "uuid"
                                                                                                        }
                                                                                                      }
                                                                                                    },
                                                                                                    "right": {
                                                                                                      "source": "schema_with_foreign_keys.orders",
                                                                                                      "field": "order_id",
                                                                                                      "type": {
                                                                                                        "name": "uuid"
                                                                                                      }
                                                                                                    }
                                                                                                  },
                                                                                                  "then": {
                                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                                    "field": "order_id",
                                                                                                    "type": {
                                                                                                      "name": "uuid"
                                                                                                    }
                                                                                                  },
                                                                                                  "else": {
                                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                                    "field": "order_user_id",
                                                                                                    "type": {
                                                                                                      "name": "uuid"
                                                                                                    }
                                                                                                  }
                                                                                                },
                                                                                                "right": {
                                                                                                  "source": "schema_with_foreign_keys.orders",
                                                                                                  "field": "order_id",
                                                                                                  "type": {
                                                                                                    "name": "uuid"
                                                                                                  }
                                                                                                }
                                                                                              },
                                                                                              "then": {
                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                "field": "order_id",
                                                                                                "type": {
                                                                                                  "name": "uuid"
                                                                                                }
                                                                                              },
                                                                                              "else": {
                                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                                "field": "order_user_id",
                                                                                                "type": {
                                                                                                  "name": "uuid"
                                                                                                }
                                                                                              }
                                                                                            },
                                                                                            "right": {
                                                                                              "source": "schema_with_foreign_keys.orders",
                                                                                              "field": "order_id",
                                                                                              "type": {
                                                                                                "name": "uuid"
                                                                                              }
                                                                                            }
                                                                                          },
                                                                                          "then": {
                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                            "field": "order_id",
                                                                                            "type": {
                                                                                              "name": "uuid"
                                                                                            }
                                                                                          },
                                                                                          "else": {
                                                                                            "source": "schema_with_foreign_keys.orders",
                                                                                            "field": "order_user_id",
                                                                                            "type": {
                                                                                              "name": "uuid"
                                                                                            }
                                                                                          }
                                                                                        },
                                                                                        "right": {
                                                                                          "source": "schema_with_foreign_keys.orders",
                                                                                          "field": "order_id",
                                                                                          "type": {
                                                                                            "name": "uuid"
                                                                                          }
                                                                                        }
                                                                                      },
                                                                                      "then": {
                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                        "field": "order_id",
                                                                                        "type": {
                                                                                          "name": "uuid"
                                                                                        }
                                                                                      },
                                                                                      "else": {
                                                                                        "source": "schema_with_foreign_keys.orders",
                                                                                        "field": "order_user_id",
                                                                                        "type": {
                                                                                          "name": "uuid"
                                                                                        }
                                                                                      }
                                                                                    },
                                                                                    "right": {
                                                                                      "source": "schema_with_foreign_keys.orders",
                                                                                      "field": "order_id",
                                                                                      "type": {
                                                                                        "name": "uuid"
                                                                                      }
                                                                                    }
                                                                                  },
                                                                                  "then": {
                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                    "field": "order_id",
                                                                                    "type": {
                                                                                      "name": "uuid"
                                                                                    }
                                                                                  },
                                                                                  "else": {
                                                                                    "source": "schema_with_foreign_keys.orders",
                                                                                    "field": "order_user_id",
                                                                                    "type": {
                                                                                      "name": "uuid"
                                                                                    }
                                                                                  }
                                                                                },
                                                                                "right": {
                                                                                  "source": "schema_with_foreign_keys.orders",
                                                                                  "field": "order_id",
                                                                                  "type": {
                                                                                    "name": "uuid"
                                                                                  }
                                                                                }
                                                                              },
                                                                              "then": {
                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                "field": "order_id",
                                                                                "type": {
                                                                                  "name": "uuid"
                                                                                }
                                                                              },
                                                                              "else": {
                                                                                "source": "schema_with_foreign_keys.orders",
                                                                                "field": "order_user_id",
                                                                                "type": {
                                                                                  "name": "uuid"
                                                                                }
                                                                              }
                                                                            },
                                                                            "right": {
                                                                              "source": "schema_with_foreign_keys.orders",
                                                                              "field": "order_id",
                                                                              "type": {
                                                                                "name": "uuid"
                                                                              }
                                                                            }
                                                                          },
                                                                          "then": {
                                                                            "source": "schema_with_foreign_keys.orders",
                                                                            "field": "order_id",
                                                                            "type": {
                                                                              "name": "uuid"
                                                                            }
                                                                          },
                                                                          "else": {
                                                                            "source": "schema_with_foreign_keys.orders",
                                                                            "field": "order_user_id",
                                                                            "type": {
                                                                              "name": "uuid"
                                                                            }
                                                                          }
                                                                        },
                                                                        "right": {
                                                                          "source": "schema_with_foreign_keys.orders",
                                                                          "field": "order_id",
                                                                          "type": {
                                                                            "name": "uuid"
                                                                          }
                                                                        }
                                                                      },
                                                                      "then": {
                                                                        "source": "schema_with_foreign_keys.orders",
                                                                        "field": "order_id",
                                                                        "type": {
                                                                          "name": "uuid"
                                                                        }
                                                                      },
                                                                      "else": {
                                                                        "source": "schema_with_foreign_keys.orders",
                                                                        "field": "order_user_id",
                                                                        "type": {
                                                                          "name": "uuid"
                                                                        }
                                                                      }
                                                                    },
                                                                    "right": {
                                                                      "source": "schema_with_foreign_keys.orders",
                                                                      "field": "order_id",
                                                                      "type": {
                                                                        "name": "uuid"
                                                                      }
                                                                    }
                                                                  },
                                                                  "then": {
                                                                    "source": "schema_with_foreign_keys.orders",
                                                                    "field": "order_id",
                                                                    "type": {
                                                                      "name": "uuid"
                                                                    }
                                                                  },
                                                                  "else": {
                                                                    "source": "schema_with_foreign_keys.orders",
                                                                    "field": "order_user_id",
                                                                    "type": {
                                                                      "name": "uuid"
                                                                    }
                                                                  }
                                                                },
                                                                "right": {
                                                                  "source": "schema_with_foreign_keys.orders",
                                                                  "field": "order_id",
                                                                  "type": {
                                                                    "name": "uuid"
                                                                  }
                                                                }
                                                              },
                                                              "then": {
                                                                "source": "schema_with_foreign_keys.orders",
                                                                "field": "order_id",
                                                                "type": {
                                                                  "name": "uuid"
                                                                }
                                                              },
                                                              "else": {
                                                                "source": "schema_with_foreign_keys.orders",
                                                                "field": "order_user_id",
                                                                "type": {
                                                                  "name": "uuid"
                                                                }
                                                              }
                                                            },
                                                            "right": {
                                                              "source": "schema_with_foreign_keys.orders",
                                                              "field": "order_id",
                                                              "type": {
                                                                "name": "uuid"
                                                              }
                                                            }
                                                          },
                                                          "then": {
                                                            "source": "schema_with_foreign_keys.orders",
                                                            "field": "order_id",
                                                            "type": {
                                                              "name": "uuid"
                                                            }
                                                          },
                                                          "else": {
                                                            "source": "schema_with_foreign_keys.orders",
                                                            "field": "order_user_id",
                                                            "type": {
                                                              "name": "uuid"
                                                            }
                                                          }
                                                        },
                                                        "right": {
                                                          "source": "schema_with_foreign_keys.orders",
                                                          "field": "order_id",
                                                          "type": {
                                                            "name": "uuid"
                                                          }
                                                        }
                                                      },
                                                      "then": {
                                                        "source": "schema_with_foreign_keys.orders",
                                                        "field": "order_id",
                                                        "type": {
                                                          "name": "uuid"
                                                        }
                                                      },
                                                      "else": {
                                                        "source": "schema_with_foreign_keys.orders",
                                                        "field": "order_user_id",
                                                        "type": {
                                                          "name": "uuid"
                                                        }
                                                      }
                                                    },
                                                    "right": {
                                                      "source": "schema_with_foreign_keys.orders",
                                                      "field": "order_id",
                                                      "type": {
                                                        "name": "uuid"
                                                      }
                                                    }
                                                  },
                                                  "then": {
                                                    "source": "schema_with_foreign_keys.orders",
                                                    "field": "order_id",
                                                    "type": {
                                                      "name": "uuid"
                                                    }
                                                  },
                                                  "else": {
                                                    "source": "schema_with_foreign_keys.orders",
                                                    "field": "order_user_id",
                                                    "type": {
                                                      "name": "uuid"
                                                    }
                                                  }
                                                },
                                                "right": {
                                                  "source": "schema_with_foreign_keys.orders",
                                                  "field": "order_id",
                                                  "type": {
                                                    "name": "uuid"
                                                  }
                                                }
                                              },
                                              "then": {
                                                "source": "schema_with_foreign_keys.orders",
                                                "field": "order_id",
                                                "type": {
                                                  "name": "uuid"
                                                }
                                              },
                                              "else": {
                                                "source": "schema_with_foreign_keys.orders",
                                                "field": "order_user_id",
                                                "type": {
                                                  "name": "uuid"
                                                }
                                              }
                                            },
                                            "right": {
                                              "source": "schema_with_foreign_keys.orders",
                                              "field": "order_id",
                                              "type": {
                                                "name": "uuid"
                                              }
                                            }
                                          },
                                          "then": {
                                            "source": "schema_with_foreign_keys.orders",
                                            "field": "order_id",
                                            "type": {
                                              "name": "uuid"
                                            }
                                          },
                                          "else": {
                                            "source": "schema_with_foreign_keys.orders",
                                            "field": "order_user_id",
                                            "type": {
                                              "name": "uuid"
                                            }
                                          }
                                        },
                                        "right": {
                                          "source": "schema_with_foreign_keys.orders",
                                          "field": "order_id",
                                          "type": {
                                            "name": "uuid"
                                          }
                                        }
                                      },
                                      "then": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_id",
                                        "type": {
                                          "name": "uuid"
                                        }
                                      },
                                      "else": {
                                        "source": "schema_with_foreign_keys.orders",
                                        "field": "order_user_id",
                                        "type": {
                                          "name": "uuid"
                                        }
                                      }
                                    },
                                    "right": {
                                      "source": "schema_with_foreign_keys.orders",
                                      "field": "order_id",
                                      "type": {
                                        "name": "uuid"
                                      }
                                    }
                                  },
                                  "then": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  },
                                  "else": {
                                    "source": "schema_with_foreign_keys.orders",
                                    "field": "order_user_id",
                                    "type": {
                                      "name": "uuid"
                                    }
                                  }
                                },
                                "right": {
                                  "source": "schema_with_foreign_keys.orders",
                                  "field": "order_id",
                                  "type": {
                                    "name": "uuid"
                                  }
                                }
                              },
                              "then": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_id",
                                "type": {
                                  "name": "uuid"
                                }
                              },
                              "else": {
                                "source": "schema_with_foreign_keys.orders",
                                "field": "order_user_id",
                                "type": {
                                  "name": "uuid"
                                }
                              }
                            },
                            "right": {
                              "source": "schema_with_foreign_keys.orders",
                              "field": "order_id",
                              "type": {
                                "name": "uuid"
                              }
                            }
                          },
                          "then": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_id",
                            "type": {
                              "name": "uuid"
                            }
                          },
                          "else": {
                            "source": "schema_with_foreign_keys.orders",
                            "field": "order_user_id",
                            "type": {
                              "name": "uuid"
                            }
                          }
                        },
                        "right": {
                          "source": "schema_with_foreign_keys.orders",
                          "field": "order_id",
                          "type": {
                            "name": "uuid"
                          }
                        }
                      },
                      "then": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_id",
                        "type": {
                          "name": "uuid"
                        }
                      },
                      "else": {
                        "source": "schema_with_foreign_keys.orders",
                        "field": "order_user_id",
                        "type": {
                          "name": "uuid"
                        }
                      }
                    },
                    "right": {
                      "source": "schema_with_foreign_keys.orders",
                      "field": "order_id",
                      "type": {
                        "name": "uuid"
                      }
                    }
                  },
                  "select": [
                    {
                      "alias": "id",
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
            new QueryJson(new DeeplyNestedIfAndEqualQuery().Value).TextValue
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
                    }
                  ],
                  "indexes": 0,
                  "rows": [
                    [
                      "00000065-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000066-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000068-0000-0000-0000-000000000000"
                    ],
                    [
                      "00000069-0000-0000-0000-000000000000"
                    ],
                    [
                      "0000006a-0000-0000-0000-000000000000"
                    ]
                  ]
                }
                """
            ).TextValue,
            new DataSetJson(new DeeplyNestedIfAndEqualQuery().Result).TextValue
        );
    }
}
